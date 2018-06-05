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
using FastShopping.Droid.ProductCatalog.Base.Mvp.View;

namespace FastShopping.Droid.ProductCatalog.Base.Mvp.Presenter
{
    public interface IProductCatalogPresenter: IPresenter<IProductCatalogView>
    {
        /// <summary>
        /// 
        /// </summary>
        void InitShoppingAction();

        /// <summary>
        /// 
        /// </summary>
        void ResetListAction();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void CatalogItemClickedAction(int item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        void FilterByCategoryAction(long categoryId);

        /// <summary>
        /// 
        /// </summary>
        void ResetCategoryByIdFilterAction();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void IncItemQuantityAction(int item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        void DecItemQuantityAction(int item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="favorite"></param>
        void SetAsFavorite(int item, bool favorite);

        /// <summary>
        /// 
        /// </summary>
        void FinlShoppingAction();

    }
}