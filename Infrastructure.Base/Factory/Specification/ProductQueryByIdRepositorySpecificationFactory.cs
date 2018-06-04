using Domain.Common.Base.Entity;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository.Specification;
using Infrastructure.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Factory.Specification
{
    internal class ProductQueryByIdRepositorySpecificationFactory : IProductQueryByIdRepositorySpecificationFactory
    {
        public IProductRepositoryByIdSpecification NewQuerySpec(long id)
        {
            return new ProductRepositoryByIdSpecification(id);
        }

        public IProductRepositoryByIdSpecification NewQuerySpec(IShoppingEntity shoppingEntity, long productId)
        {
            return new ShoppingProductRepositoryByIdSpecification(shoppingEntity.Id, productId);
        }
    }
}
