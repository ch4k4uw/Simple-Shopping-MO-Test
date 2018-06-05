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

namespace FastShopping.Droid.ProductCatalog.Base.Mvp.View
{
    public interface IProductCatalogView: IView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productListItems"></param>
        void SetProductCatalog(IList<ProductListItem> productListItems);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCategories"></param>
        void SetProductCategoryFilterOptions(IList<ProductCategory> productCategories);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="productListItem"></param>
        void UpdateProductCatalogItem(int item, ProductListItem productListItem);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        void ShowProductDetail(long productId);

        /// <summary>
        /// 
        /// </summary>
        void ShowShoppingFinishedNotification();

    }
}