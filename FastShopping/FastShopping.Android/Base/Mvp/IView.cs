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
    public interface IView
    {
        /// <summary>
        /// 
        /// </summary>
        void ShowProgress();

        /// <summary>
        /// 
        /// </summary>
        void HideProgress();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void ShowErrorMessage(string message);
    }
}