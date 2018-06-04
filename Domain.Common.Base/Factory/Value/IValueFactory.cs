using Domain.Common.Base.Value;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Factory.Value
{
    public interface IValueFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="photo"></param>
        /// <param name="price"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IProductDetailValue NewProductDetail(string name, string description, string photo, double price, long categoryId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        IProductPromotionDetailValue NewProductPromotionDetail(int min, float discount);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IProductPromotionValue EmptyProductPromotion();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="categoryId"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        IProductPromotionValue NewProductPromotion(string name, long categoryId, IList<IProductPromotionDetailValue> details);
    }
}
