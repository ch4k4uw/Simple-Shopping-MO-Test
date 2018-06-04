using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Rest
{
    public interface IRestApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<IProduct> ListProducts();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<IPromotion> ListPromotions();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<ICategory> ListCategories();
    }
}
