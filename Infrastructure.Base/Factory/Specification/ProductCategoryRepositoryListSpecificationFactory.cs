using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Domain.ProductCatalog.Base.Factory.Specification;
using Domain.ProductCatalog.Base.Repository.Specification;
using Infrastructure.Base.Repository.Specification;

namespace Infrastructure.Base.Factory.Specification
{
    internal class ProductCategoryRepositoryListSpecificationFactory : IProductCategoryRepositoryListSpecificationFactory
    {
        public IProductCategoryRepositoryListSpecification NewListSpec()
        {
            return new ProductCategoryRepositoryListSpecification();
        }
    }
}