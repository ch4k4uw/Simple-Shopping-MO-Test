using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Application.Dto.Result;
using RestSharp;

namespace FastShopping.Droid.ProductCatalog.Adapter
{
    public class ProductCatalogAdapter : RecyclerView.Adapter, View.IOnClickListener
    {
        private readonly IList<ProductListItem> list;
        private readonly Action<int> addAction;
        private readonly Action<int> removeAction;
        private readonly Action<int, bool> favoriteAction;
        private readonly Context context;
        
        public ProductCatalogAdapter(Context context, IList<ProductListItem> list, Action<int> addAction, Action<int> removeAction, Action<int, bool> favoriteAction)
        {
            this.context = context;
            this.list = list;
            this.addAction = addAction;
            this.removeAction = removeAction;
            this.favoriteAction = favoriteAction;
        }

        public override int ItemCount => list?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = list[position];
            var pcHolder = holder as ProductCatalogViewHolder;

            pcHolder.Discount.Visibility = item.Discount == 0 ? ViewStates.Gone : ViewStates.Visible;
            pcHolder.Promotion.Visibility = !item.IsShowPromotion ? ViewStates.Gone : ViewStates.Visible;

            pcHolder.Promotion.Text = item.PromotionName;
            pcHolder.Name.Text = item.Name;
            pcHolder.Discount.Text = item.Discount.ToString("F1", CultureInfo.InvariantCulture) + "%";
            pcHolder.Price.Text = "R$ " + (item.Price * (item.Discount > 0 ? 1.0f - (item.Discount / 100.0) : 1)).ToString("F2", CultureInfo.InvariantCulture);
            pcHolder.Quantity.Text = $"{item.Quantity}";
            pcHolder.ChangeCheckState(item.IsFavorite);

            DownloadImage(pcHolder.Img, item.Photo);

        }

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.add)
            {
                addAction.Invoke((int)v.Tag);
            } else if(v.Id == Resource.Id.remove)
            {
                removeAction.Invoke((int)v.Tag);
            } else if(v.Id == Resource.Id.favorite)
            {
                favoriteAction.Invoke((int)v.Tag, (v as RatingBar).Rating == 1);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.product_catalog_item, parent, false);
            return new ProductCatalogViewHolder(view, this);
        }

        private IDictionary<string, ImageView> pendingDownloadsImgIndex = new Dictionary<string, ImageView>();
        private IDictionary<string, IDisposable> pendingDownloadsSubscriptionIndex = new Dictionary<string, IDisposable>();
        private void DownloadImage(ImageView imageView, string url)
        {
            var oldUrl = (string)imageView.Tag;
            if(oldUrl != null)
            {
                pendingDownloadsSubscriptionIndex[oldUrl].Dispose();
                pendingDownloadsSubscriptionIndex.Remove(oldUrl);
                pendingDownloadsImgIndex.Remove(oldUrl);

                imageView.Tag = null;
            }

            imageView.Tag = url;
            pendingDownloadsImgIndex.Add(url, imageView);

            imageView.SetImageDrawable(null);
            pendingDownloadsSubscriptionIndex.Add(url,
                Observable
                    .Start(() =>
                    {
                        var localUri = GetURIFromCache(url);
                        var exists = localUri != "" && File.Exists(localUri);
                        if (localUri == "" || !exists)
                        {
                            if(localUri != "" && !exists)
                            {
                                RemoveFromCache(url);
                            }

                            var uri = new Uri(url);

                            RestClient restClient = new RestClient(url);
                            var fileBytes = restClient.DownloadData(new RestRequest(Method.GET));

                            if (fileBytes != null)
                            {
                                var path = Path.Combine(context.CacheDir.AbsolutePath, Path.GetFileName(uri.LocalPath));
                                File.WriteAllBytes(path, fileBytes);
                                var path2 = Path.Combine(context.GetExternalFilesDir(null).AbsolutePath, Path.GetFileName(uri.LocalPath));
                                File.WriteAllBytes(path2, fileBytes);

                                PersistCache(url, path);

                                localUri = path;
                            }
                        }

                        return localUri;
                    })
                    .ObserveOn(ReactiveUI.HandlerScheduler.MainThreadScheduler)
                    .SubscribeOn(DefaultScheduler.Instance/*NewThreadScheduler.Default*/)
                    .Subscribe(localUri => 
                    {
                        pendingDownloadsSubscriptionIndex.Remove(url);
                        pendingDownloadsImgIndex.Remove(url);

                        imageView.Tag = null;

                        imageView.SetImageURI(Android.Net.Uri.Parse(localUri));
                    }, err => {
                        pendingDownloadsSubscriptionIndex.Remove(url);
                        pendingDownloadsImgIndex.Remove(url);

                        imageView.Tag = null;
                    })
                );
        }

        private void PersistCache(string url, string path)
        {
            lock(context)
            {
                ISharedPreferences shp = context.GetSharedPreferences("image_cache", FileCreationMode.Private);
                shp.Edit()
                    .PutString(url, path)
                    .Commit();
            }
        }

        private void RemoveFromCache(string url)
        {
            lock (context)
            {
                ISharedPreferences shp = context.GetSharedPreferences("image_cache", FileCreationMode.Private);
                shp.Edit()
                    .Remove(url)
                    .Commit();

            }
        }

        private string GetURIFromCache(string url)
        {
            lock (context)
            {
                ISharedPreferences shp = context.GetSharedPreferences("image_cache", FileCreationMode.Private);
                return shp.GetString(url, "");
            }
        }
    }
}