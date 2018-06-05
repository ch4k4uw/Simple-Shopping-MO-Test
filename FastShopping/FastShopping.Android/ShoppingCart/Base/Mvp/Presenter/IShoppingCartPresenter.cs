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
using FastShopping.Droid.Base.Mvp;
using FastShopping.Droid.ShoppingCart.Base.Mvp.View;

namespace FastShopping.Droid.ShoppingCart.Base.Mvp.Presenter
{
    public interface IShoppingCartPresenter: IPresenter<IShoppingCartView>
    {
        /// <summary>
        /// 
        /// </summary>
        void LoadCartAction();

        /// <summary>
        /// 
        /// </summary>
        void ReloadCartAction();

        /// <summary>
        /// 
        /// </summary>
        void FinalizeShoppingAction();
    }
}