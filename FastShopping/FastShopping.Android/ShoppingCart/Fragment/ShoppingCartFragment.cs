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
using Android.Views;
using Android.Widget;
using Application.Dto.Result;
using FastShopping.Droid.ShoppingCart.Adapter;
using FastShopping.Droid.ShoppingCart.Base.Mvp.Presenter;
using FastShopping.Droid.ShoppingCart.Base.Mvp.View;
using Unity;

namespace FastShopping.Droid.ShoppingCart.Fragment
{
    public class ShoppingCartFragment : Android.Support.V4.App.Fragment, IShoppingCartView
    {
        private IShoppingCartPresenter presenter;
        private ProgressBar progress;
        private RecyclerView recycler;
        private Android.Support.V7.Widget.Toolbar toolbar;
        private Button finalizeShopping;
        private TextView quantity;
        private TextView total;

        private bool alreadyStarted;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
            HasOptionsMenu = true;

            presenter = App.Container.Resolve<IShoppingCartPresenter>();
            presenter.View = this;

            alreadyStarted = false;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.shopping_cart, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            progress = view.FindViewById<ProgressBar>(Resource.Id.progressbar);
            recycler = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            finalizeShopping = view.FindViewById<Button>(Resource.Id.finalize);
            quantity = view.FindViewById<TextView>(Resource.Id.quantity);
            total = view.FindViewById<TextView>(Resource.Id.total);

            recycler.HasFixedSize = true;
            recycler.SetLayoutManager(
                    new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false)
            );

            finalizeShopping.Click += delegate
            {
                presenter.FinalizeShoppingAction();
            };

            //
            var activity = Activity as AppCompatActivity;
            toolbar.SetLogo(Resource.Mipmap.icon);
            toolbar.SetTitle(Resource.String.app_name);
            toolbar.SetSubtitle(Resource.String.app_short_desc);

            activity.SetSupportActionBar(toolbar);
            activity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            activity.SupportActionBar.SetHomeButtonEnabled(true);

            if(!alreadyStarted)
            {
                presenter.LoadCartAction();
            } else
            {
                presenter.ReloadCartAction();
            }
        }

        public void HideProgress()
        {
            progress.Visibility = ViewStates.Gone;
        }

        public void RestartApp()
        {
            var mainActivity = new Intent(Activity, typeof(MainActivity));
            mainActivity.SetFlags(ActivityFlags.ClearTop);
            Activity.StartActivity(mainActivity);
        }

        public void SetCart(IList<ProductListItem> productListItems)
        {
            recycler.SetAdapter(new ShoppingCartAdapter(Activity, productListItems));
            alreadyStarted = true;
        }

        public void ShowErrorMessage(string message)
        {
            Toast.MakeText(Activity, message, ToastLength.Long)
                .Show();
        }

        public void ShowProgress()
        {
            progress.Visibility = ViewStates.Visible;
        }

        public void ShowShoppingFinalizedNotification()
        {
            Toast.MakeText(Activity, Resource.String.shopping_finished_message, ToastLength.Long)
                .Show();
        }

        public void UpdateTotal(int quantity, double total)
        {
            this.quantity.Text = $"{quantity} UN";
            this.total.Text = total.ToString("C2");
        }
    }
}