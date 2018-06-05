using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Application.Dto.Result;
using FastShopping.Droid.ProductCatalog.Adapter;
using FastShopping.Droid.ProductCatalog.Base;
using FastShopping.Droid.ProductCatalog.Base.Mvp.Presenter;
using FastShopping.Droid.ProductCatalog.Base.Mvp.View;
using Unity;

namespace FastShopping.Droid.ProductCatalog.Fragment
{
    public class ProductCatalogFragment : Android.Support.V4.App.Fragment, IProductCatalogView, IProductByCategoryFilterApplyer
    {
        private IProductCatalogPresenter presenter;
        private ProgressBar progress;
        private RecyclerView recycler;
        private Android.Support.V7.Widget.Toolbar toolbar;

        private bool alreadyStarted;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
            HasOptionsMenu = true;
            
            presenter = App.Container.Resolve<IProductCatalogPresenter>();
            presenter.View = this;

            alreadyStarted = false;

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.product_catalog, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            progress = view.FindViewById<ProgressBar>(Resource.Id.progressbar);
            recycler = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            recycler.HasFixedSize = true;
            recycler.SetLayoutManager(
                    new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false)
            );

            //
            var activity = Activity as AppCompatActivity;
            toolbar.SetLogo(Resource.Mipmap.icon);
            toolbar.SetTitle(Resource.String.app_name);
            toolbar.SetSubtitle(Resource.String.app_short_desc);

            activity.SetSupportActionBar(toolbar);
            activity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            activity.SupportActionBar.SetHomeButtonEnabled(true);
            //

            if (!alreadyStarted)
            {
                presenter.InitShoppingAction();
            } else
            {
                presenter.ResetListAction();
            }
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.catalog, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.filter:
                    (Activity as IProductCategoryFilterPresenter)
                        .ShowFilterOptions();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        public void SetProductCatalog(IList<ProductListItem> productListItems)
        {
            recycler.SetAdapter(new ProductCatalogAdapter(Activity, productListItems, presenter.IncItemQuantityAction, presenter.DecItemQuantityAction, presenter.SetAsFavorite));
            alreadyStarted = true;
        }

        public void ShowErrorMessage(string message)
        {
            Toast.MakeText(Activity, message, ToastLength.Long)
                .Show();
        }

        public void ShowProductDetail(long productId)
        {
            
        }

        public void ShowProgress()
        {
            progress.Visibility = ViewStates.Visible;
        }

        public void HideProgress()
        {
            progress.Visibility = ViewStates.Gone;
        }

        public void ShowShoppingFinishedNotification()
        {
            Toast.MakeText(Activity, Resource.String.shopping_finished_message, ToastLength.Long)
                .Show();
        }

        public void UpdateProductCatalogItem(int item, ProductListItem productListItem)
        {
            var adapter = recycler.GetAdapter() as ProductCatalogAdapter;
            adapter.NotifyItemChanged(item);
        }

        public void ApplyCategoryFilter(long id)
        {
            presenter.FilterByCategoryAction(id);
        }

        public void SetProductCategoryFilterOptions(IList<ProductCategory> productCategories)
        {
            (Activity as IProductCategoryFilterPresenter)
                .SetFilterOptions(productCategories);
        }
    }
}