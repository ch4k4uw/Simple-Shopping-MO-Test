using Domain.Common.Base.Entity;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository.Specification;
using Infrastructure.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Factory.Specification
{
    internal class ShoppingItemByIdRepositorySpecificationFactory : IShoppingItemByIdRepositorySpecificationFactory
    {
        public IShoppingItemByIdRepositorySpecification NewQuerySpec(IShoppingEntity shoppingEntity, long shoppingItemId)
        {
            return new ShoppingItemQueryByIdRepositorySpecification(shoppingEntity.Id, shoppingItemId);
        }

        public IShoppingItemByIdRepositorySpecification NewUpdateSpec(IShoppingEntity shoppingEntity, long shoppingItemId)
        {
            return new ShoppingItemUpdateByIdRepositorySpecification(shoppingEntity.Id, shoppingItemId);
        }
    }
}
