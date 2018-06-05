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
using Application.Dto.Result;
using FastShopping.Droid.Base.Mvp;

namespace FastShopping.Droid.ShoppingCart.Base.Mvp.View
{
    public interface IShoppingCartView: IView
    {
        void SetCart(IList<ProductListItem> productListItems);

        /// <summary>
        /// 
        /// </summary>
        void ShowShoppingFinalizedNotification();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="total"></param>
        void UpdateTotal(int quantity, double total);

        /// <summary>
        /// 
        /// </summary>
        void RestartApp();
    }
}