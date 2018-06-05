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

namespace FastShopping.Droid.ProductCatalog.Base
{
    public interface IProductCategoryFilterPresenter
    {
        /// <summary>
        /// 
        /// </summary>
        void ShowFilterOptions();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categories"></param>
        void SetFilterOptions(IList<ProductCategory> categories);
    }
}