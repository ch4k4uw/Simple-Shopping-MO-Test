using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository.Specification;
using Infrastructure.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Factory.Specification
{
    internal class ProductQueryRepositorySpecificationFactory : IProductQueryRepositorySpecificationFactory
    {
        public IProductRepositoryListSpecification NewCartSpec(long shoppingId)
        {
            return new ProductRepositoryCartSpecification(shoppingId);
        }

        public IProductRepositoryListSpecification NewCatalogSpec(long shoppingId)
        {
            return new ProductRepositoryListCatalogSpecification(shoppingId);
        }
    }
}
