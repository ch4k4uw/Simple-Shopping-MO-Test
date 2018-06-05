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
using Domain.Common.Base.Application.Service;
using Domain.ProductCatalog.Base.Application;
using FastShopping.Droid.ProductCatalog.Base.Mvp.Presenter;
using FastShopping.Droid.ProductCatalog.Base.Mvp.View;

namespace FastShopping.Droid.ProductCatalog.Presenter
{
    public class ProductCatalogPresenter : IProductCatalogPresenter
    {
        private readonly IInitializeShoppingApplicationService initShoppingSvc;
        private readonly IFinalizeShoppingApplicationService finlShoppingSvc;
        private readonly IListProductsApplicationService<ProductListItem> listProductsSvc;
        private readonly IIncProductQuantityApplicationService<ProductDetail> incProdQuantitySvc;
        private readonly IDecProductQuantityApplicationService<ProductDetail> decProdQuantitySvc;
        private readonly ISetAsFavoriteApplicationService setAsFavoriteSvc;
        private readonly IListCategoryApplicationService<ProductCategory> listCategoriesSvc;

        private IList<ProductListItem> origList;
        private IList<ProductListItem> filteredList;
        private IList<ProductCategory> categories;

        private long categoryIdFilter;

        public ProductCatalogPresenter(IInitializeShoppingApplicationService initShoppingSvc, IFinalizeShoppingApplicationService finlShoppingSvc, IListProductsApplicationService<ProductListItem> listProductsSvc, IIncProductQuantityApplicationService<ProductDetail> incProdQuantitySvc, IDecProductQuantityApplicationService<ProductDetail> decProdQuantitySvc, ISetAsFavoriteApplicationService setAsFavoriteSvc, IListCategoryApplicationService<ProductCategory> listCategoriesSvc)
        {
            this.initShoppingSvc = initShoppingSvc;
            this.finlShoppingSvc = finlShoppingSvc;
            this.listProductsSvc = listProductsSvc;
            this.incProdQuantitySvc = incProdQuantitySvc;
            this.decProdQuantitySvc = decProdQuantitySvc;
            this.setAsFavoriteSvc = setAsFavoriteSvc;
            this.listCategoriesSvc = listCategoriesSvc;
            origList = null;
            filteredList = null;
            categoryIdFilter = -1;

        }

        public IProductCatalogView View { get; set; }

        public void InitShoppingAction()
        {
            View?.ShowProgress();
            initShoppingSvc.Initialize(() => 
            {
                Action<Exception> errorHandler = err =>
                {
                    View?.ShowErrorMessage(err.Message ?? "An error ocurred");
                    View?.HideProgress();
                };

                listCategoriesSvc.List(categories => 
                {
                    this.categories = categories;
                    listProductsSvc.List(products =>
                    {
                        origList = products;
                        FilterOrigList();

                        View?.SetProductCategoryFilterOptions(categories);
                        View?.SetProductCatalog(filteredList);

                        View?.HideProgress();
                    },
                    errorHandler);

                }, errorHandler);
            });
        }

        public void ResetListAction()
        {
            View?.SetProductCatalog(filteredList);
            View?.SetProductCategoryFilterOptions(categories);
        }

        public void CatalogItemClickedAction(int item)
        {
            View?.ShowProductDetail(filteredList[item].Id);
        }

        public void FilterByCategoryAction(long categoryId)
        {
            categoryIdFilter = categoryId;
            FilterOrigList();

            View?.SetProductCatalog(filteredList);
        }

        public void IncItemQuantityAction(int item)
        {
            View?.ShowProgress();
            incProdQuantitySvc.Inc(filteredList[item].Id, product =>
            {

                filteredList[item].Quantity = product.Quantity;
                filteredList[item].Discount = product.Discount;
                filteredList[item].IsFavorite = product.IsFavorite;

                View?.UpdateProductCatalogItem(item, filteredList[item]);
                View?.HideProgress();
            }, 
            err => 
            {
                View?.ShowErrorMessage(err.Message ?? "An error ocurred");
                View?.HideProgress();
            });
        }

        public void DecItemQuantityAction(int item)
        {
            View?.ShowProgress();
            decProdQuantitySvc.Dec(filteredList[item].Id, product =>
            {

                filteredList[item].Quantity = product.Quantity;
                filteredList[item].Discount = product.Discount;
                filteredList[item].IsFavorite = product.IsFavorite;

                View?.UpdateProductCatalogItem(item, filteredList[item]);
                View?.HideProgress();
            },
            err =>
            {
                View?.ShowErrorMessage(err.Message ?? "An error ocurred");
                View?.HideProgress();
            });
        }

        public void FinlShoppingAction()
        {
            View?.ShowProgress();
            finlShoppingSvc.Finalyze(() => 
            {
                View?.ShowShoppingFinishedNotification();
                View?.HideProgress();

                InitShoppingAction();
            });
        }

        public void ResetCategoryByIdFilterAction()
        {
            categoryIdFilter = -1;
            FilterOrigList();

            View?.SetProductCatalog(filteredList);
        }

        public void SetAsFavorite(int item, bool favorite)
        {
            View?.ShowProgress();
            setAsFavoriteSvc.SetAsFavorite(filteredList[item].Id, favorite, () =>
            {
                View?.HideProgress();
            });
        }

        private void FilterOrigList()
        {
            var lastPromotion = "";
            filteredList = origList
                .Where(x => categoryIdFilter == -1 || x.Category.Id == categoryIdFilter)
                .OrderBy(x => x.PromotionName ?? "")
                .ThenBy(x => x.Name)
                .Select(x =>
                {
                    if(x.PromotionName != lastPromotion)
                    {
                        x.IsShowPromotion = true;
                        lastPromotion = x.PromotionName;
                    }
                    else
                    {
                        x.IsShowPromotion = false;
                    }
                    return x;
                })
                .ToList();
        }
    }
}