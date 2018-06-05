using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using FastShopping.Droid.ProductCatalog.Fragment;
using Android.Support.V7.Widget;
using FastShopping.Droid.ProductCatalog.Base;
using Application.Dto.Result;
using System.Collections.Generic;
using FastShopping.Droid.ProductCatalog.Adapter;
using System.Linq;
using Android.Support.V4.Widget;

namespace FastShopping.Droid
{
    [Activity(Label = "FastShopping")]
    public class MainActivity : AppCompatActivity, IProductCategoryFilterPresenter
    {
        private DrawerLayout drawerLayout;
        private RecyclerView recycler;
        private IList<ProductCategory> recyclerDataSource;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if(Intent.HasExtra("error_message"))
            {
                Toast.MakeText(this, Intent.Extras.GetString("error_message"), ToastLength.Long)
                    .Show();
                Finish();
                return;
            }

            SetContentView(Resource.Layout.product_catalog_container);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            recycler = FindViewById<RecyclerView>(Resource.Id.drawer_list);
            recycler.SetLayoutManager(new LinearLayoutManager(this));

            if (bundle != null)
            {
                return;
            }

            var fragment = new ProductCatalogFragment();
            var tag = "list";

            SupportFragmentManager
                .BeginTransaction()
                .Add(Resource.Id.fragment_container, fragment, tag)
                .Commit();

        }

        public void SetFilterOptions(IList<ProductCategory> categories)
        {
            recyclerDataSource = new List<ProductCategory>();
            recyclerDataSource.Add(new ProductCategory
            {
                Id = -1,
                Name = GetString(Resource.String.all_categories_menu_item)
            });

            categories.OrderBy(x => x.Name).Select(x => 
            {
                recyclerDataSource.Add(x);
                return x;
            }).ToList();


            recycler.SetAdapter(new ProductCategoryAdapter(this, recyclerDataSource, item => 
            {
                (SupportFragmentManager.FindFragmentByTag("list") as IProductByCategoryFilterApplyer)
                    .ApplyCategoryFilter(recyclerDataSource[item].Id);
                drawerLayout.CloseDrawer((int)GravityFlags.Right, true);
            }));
        }

        public void ShowFilterOptions()
        {
            drawerLayout.OpenDrawer((int)GravityFlags.Right, true);
        }
    }
}

