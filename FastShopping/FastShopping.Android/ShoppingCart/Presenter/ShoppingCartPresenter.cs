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
using Domain.ProductCatalog.Base.Application;
using Domain.ShoppingCart.Base.Application;
using FastShopping.Droid.ShoppingCart.Base.Mvp.Presenter;
using FastShopping.Droid.ShoppingCart.Base.Mvp.View;

namespace FastShopping.Droid.ShoppingCart.Presenter
{
    public class ShoppingCartPresenter : IShoppingCartPresenter
    {
        private readonly IFinalizeShoppingApplicationService finlShoppingSvc;
        private readonly IShoppingCartApplicationService<ProductListItem> shpCartSvc;
        private IList<ProductListItem> origList;

        public ShoppingCartPresenter(IShoppingCartApplicationService<ProductListItem> shpCartSvc, IFinalizeShoppingApplicationService finlShoppingSvc)
        {
            this.shpCartSvc = shpCartSvc;
            this.finlShoppingSvc = finlShoppingSvc;
        }

        public IShoppingCartView View { get; set; }

        public void FinalizeShoppingAction()
        {
            View?.ShowProgress();

            Action<Exception> errorHandler = err =>
            {
                View?.ShowErrorMessage(err.Message ?? "An error ocurred");
                View?.HideProgress();
            };

            finlShoppingSvc.Finalyze(() =>
            {
                View?.HideProgress();
                View?.ShowShoppingFinalizedNotification();
                View?.RestartApp();
            });
        }

        public void LoadCartAction()
        {
            View?.ShowProgress();

            Action<Exception> errorHandler = err =>
            {
                View?.ShowErrorMessage(err.Message ?? "An error ocurred");
                View?.HideProgress();
            };

            shpCartSvc.List(products =>
            {
                origList = products;

                UpdateTotal();
                View?.SetCart(origList);

                View?.HideProgress();
            },
            errorHandler);
        }

        public void ReloadCartAction()
        {
            View?.SetCart(origList);
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            var total = origList.Sum(x => x.Price * x.Quantity);
            var quantity = origList.Sum(x => x.Quantity);

            View?.UpdateTotal(quantity, total);
        }
    }
}