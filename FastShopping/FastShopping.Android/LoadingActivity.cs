using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace FastShopping.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon", MainLauncher = true, NoHistory = true)]
    public class LoadingActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loading);

            if(App.AlreadyStartedUp)
            {
                App.StartSystem(this);
            }
        }
    }
}