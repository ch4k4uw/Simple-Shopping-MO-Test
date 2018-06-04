using Domain.Common.Aggregation;
using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Factory.Entity;
using Domain.Common.Base.Value;
using Domain.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Factory.Entity
{
    public class EntityFactory : IEntityFactory
    {
        public IProductAggregation EmptyProduct()
        {
            return ProductAggregation.Empty;
        }

        public IProductCategoryEntity EmptyProductCategory()
        {
            return ProductCategoryEntity.Empty;
        }

        public IShoppingEntity EmptyShopping()
        {
            return ShoppingEntity.Empty;
        }

        public IShoppingItemEntity EmptyShoppingItem()
        {
            return ShoppingItemEntity.Empty;
        }

        public IFavoriteProductEntity NewFavoriteProductEntity(long id, bool favorite)
        {
            return new FavoriteProductEntity(id, favorite);
        }

        public IProductAggregation NewProduct(long id, IProductDetailValue detail, IProductPromotionValue promotion, IProductCategoryEntity productCategory,  float discount, int quantity, bool favorite)
        {
            return new ProductAggregation(id, detail, promotion, productCategory, discount, quantity, favorite);
        }

        public IProductCategoryEntity NewProductCategory(long id, string name)
        {
            return new ProductCategoryEntity(id, name);
        }

        public IShoppingEntity NewShopping()
        {
            return new ShoppingEntity(0, DateTime.Now, DateTime.MinValue);
        }

        public IShoppingEntity NewShopping(long id, DateTime creation, DateTime finished)
        {
            return new ShoppingEntity(id, creation, finished);
        }

        public IShoppingItemEntity NewShoppingItem(IShoppingEntity shoppingEntity, IProductAggregation productAggregation)
        {
            return new ShoppingItemEntity(shoppingEntity, productAggregation);
        }
    }
}
