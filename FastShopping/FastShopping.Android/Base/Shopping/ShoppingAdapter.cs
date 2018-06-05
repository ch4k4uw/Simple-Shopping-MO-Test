using System;
using System.Collections.Generic;
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
using RestSharp;

namespace FastShopping.Droid.Base.Shopping
{
    public abstract class ShoppingAdapter : RecyclerView.Adapter
    {
        protected readonly Context context;

        public ShoppingAdapter(Context context)
        {
            this.context = context;
        }

        private IDictionary<string, ImageView> pendingDownloadsImgIndex = new Dictionary<string, ImageView>();
        private IDictionary<string, IDisposable> pendingDownloadsSubscriptionIndex = new Dictionary<string, IDisposable>();
        protected void DownloadImage(ImageView imageView, string url)
        {
            var oldUrl = (string)imageView.Tag;
            if (oldUrl != null)
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
                            if (localUri != "" && !exists)
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
            lock (context)
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