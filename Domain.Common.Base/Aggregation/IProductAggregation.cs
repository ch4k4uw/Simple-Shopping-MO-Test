using System;
using System.Collections.Generic;
using System.Text;
using Domain.Base.Entity;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.Common.Base.Value;

namespace Domain.Common.Base.Aggregation
{
    public interface IProductAggregation: ILongIdEntity
    {
        /// <summary>
        /// 
        /// </summary>
        IProductDetailValue Detail { get; }

        /// <summary>
        /// 
        /// </summary>
        IProductPromotionValue Promotion { get; }

        /// <summary>
        /// 
        /// </summary>
        IProductCategoryEntity Category { get; }

        /// <summary>
        /// 
        /// </summary>
        float Discount { get; }

        /// <summary>
        /// 
        /// </summary>
        int Quantity { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsFavorite { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingQueryRepository"></param>
        /// <param name="shoppingQueryRepositorySpecificationFactory"></param>
        /// <param name="shoppingItemRepository"></param>
        /// <param name="shoppingItemByIdRepositorySpecificationFactory"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void IncQuantity(IShoppingRepository shoppingQueryRepository, IShoppingQueryRepositorySpecificationFactory shoppingQueryRepositorySpecificationFactory, IShoppingItemRepository shoppingItemRepository, IShoppingItemByIdRepositorySpecificationFactory shoppingItemByIdRepositorySpecificationFactory, Action<IProductAggregation> success, Action<System.Exception> error);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingQueryRepository"></param>
        /// <param name="shoppingQueryRepositorySpecificationFactory"></param>
        /// <param name="shoppingItemRepository"></param>
        /// <param name="shoppingItemByIdRepositorySpecificationFactory"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void DecQuantity(IShoppingRepository shoppingQueryRepository, IShoppingQueryRepositorySpecificationFactory shoppingQueryRepositorySpecificationFactory, IShoppingItemRepository shoppingItemRepository, IShoppingItemByIdRepositorySpecificationFactory shoppingItemByIdRepositorySpecificationFactory, Action<IProductAggregation> success, Action<System.Exception> error);
        
    }
}
