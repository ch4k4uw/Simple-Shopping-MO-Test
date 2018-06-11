
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using FastShopping.Droid.ProductCatalog.Fragment;

namespace FastShopping.Droid
{
    [Activity(Label = "FastShopping")]
    public class MainActivity : AppCompatActivity
    {
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

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Android.Resource.Id.Home:
                    var fragment = SupportFragmentManager.FindFragmentByTag("cart");
                    if(fragment == null)
                    {
                        Finish();
                    } else
                    {
                        SupportFragmentManager.PopBackStack();
                    }
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

