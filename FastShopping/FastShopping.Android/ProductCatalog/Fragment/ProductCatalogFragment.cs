using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Application.Dto.Result;
using FastShopping.Droid.ProductCatalog.Adapter;
using FastShopping.Droid.ProductCatalog.Base;
using FastShopping.Droid.ProductCatalog.Base.Mvp.Presenter;
using FastShopping.Droid.ProductCatalog.Base.Mvp.View;
using FastShopping.Droid.ShoppingCart.Fragment;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace FastShopping.Droid.ProductCatalog.Fragment
{
    public class ProductCatalogFragment : Android.Support.V4.App.Fragment, IProductCatalogView
    {
        private IProductCatalogPresenter presenter;
        private ProgressBar progress;
        private RecyclerView recycler;
        private Android.Support.V7.Widget.Toolbar toolbar;
        private Button shoppingCart;
        private DrawerLayout drawerLayout;
        private RecyclerView drawerRecycler;

        private IList<ProductCategory> recyclerDataSource;

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
            shoppingCart = view.FindViewById<Button>(Resource.Id.shopping_cart);
            drawerLayout = view.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawerRecycler = view.FindViewById<RecyclerView>(Resource.Id.drawer_list);

            shoppingCart.Click += delegate
            {
                FragmentManager
                    .BeginTransaction()
                    .Replace(Resource.Id.fragment_container, new ShoppingCartFragment(), "cart")
                    .AddToBackStack(null)
                    .Commit();
            };

            recycler.HasFixedSize = true;
            recycler.SetLayoutManager(
                    new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false)
            );

            drawerRecycler.HasFixedSize = true;
            drawerRecycler.SetLayoutManager(new LinearLayoutManager(Activity));

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
                    drawerLayout.OpenDrawer((int)GravityFlags.Right, true);
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

        public void ShowShoppingFinalizedNotification()
        {
            Toast.MakeText(Activity, Resource.String.shopping_finished_message, ToastLength.Long)
                .Show();
        }

        public void UpdateProductCatalogItem(int item, ProductListItem productListItem)
        {
            var adapter = recycler.GetAdapter() as ProductCatalogAdapter;
            adapter.NotifyItemChanged(item);
        }

        public void SetProductCategoryFilterOptions(IList<ProductCategory> productCategories)
        {
            recyclerDataSource = new List<ProductCategory>();
            recyclerDataSource.Add(new ProductCategory
            {
                Id = -1,
                Name = GetString(Resource.String.all_categories_menu_item)
            });

            productCategories.OrderBy(x => x.Name).Select(x =>
            {
                recyclerDataSource.Add(x);
                return x;
            }).ToList();


            drawerRecycler.SetAdapter(new ProductCategoryAdapter(Activity, recyclerDataSource, item =>
            {
                presenter.FilterByCategoryAction(recyclerDataSource[item].Id);
                drawerLayout.CloseDrawer((int)GravityFlags.Right, true);
            }));
        }

        public void UpdateTotal(double total)
        {
            var prefix = GetString(Resource.String.shopping_cart_button_prefix);
            shoppingCart.Text = $"{prefix} {total.ToString("C2")}";

            shoppingCart.Enabled = total > 0;

        }
    }
}