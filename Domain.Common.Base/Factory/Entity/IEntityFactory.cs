using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Value;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Factory.Entity
{
    public interface IEntityFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IProductAggregation EmptyProduct();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="detail"></param>
        /// <param name="promotion"></param>
        /// <param name="productCategory"></param>
        /// <param name="discount"></param>
        /// <param name="quantity"></param>
        /// <param name="favorite"></param>
        /// <returns></returns>
        IProductAggregation NewProduct(long id, IProductDetailValue detail, IProductPromotionValue promotion, IProductCategoryEntity productCategory, float discount, int quantity, bool favorite);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IShoppingItemEntity EmptyShoppingItem();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingEntity"></param>
        /// <param name="productAggregation"></param>
        /// <returns></returns>
        IShoppingItemEntity NewShoppingItem(IShoppingEntity shoppingEntity, IProductAggregation productAggregation);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IShoppingEntity EmptyShopping();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IShoppingEntity NewShopping();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creation"></param>
        /// <param name="finished"></param>
        /// <returns></returns>
        IShoppingEntity NewShopping(long id, DateTime creation, DateTime finished);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IProductCategoryEntity EmptyProductCategory();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IProductCategoryEntity NewProductCategory(long id, string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="favorite"></param>
        /// <returns></returns>
        IFavoriteProductEntity NewFavoriteProductEntity(long id, bool favorite);

    }
}
