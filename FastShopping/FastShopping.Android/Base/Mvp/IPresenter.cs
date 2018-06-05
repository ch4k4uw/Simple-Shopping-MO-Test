using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FastShopping.Droid.Base.Mvp
{
    public interface IPresenter<T> where T: IView
    {
        /// <summary>
        /// 
        /// </summary>
        T View { get; set; }
    }
}