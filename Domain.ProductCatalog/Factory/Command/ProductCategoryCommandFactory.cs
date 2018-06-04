using Domain.ProductCatalog.Base.Command;
using Domain.ProductCatalog.Base.Factory.Command;
using Domain.ProductCatalog.Base.Factory.Specification;
using Domain.ProductCatalog.Base.Repository;
using Domain.ProductCatalog.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Factory.Command
{
    public class ProductCategoryCommandFactory : IProductCategoryCommandFactory
    {
        private readonly IProductCategoryQueryRepository repository;
        private readonly IProductCategoryRepositoryListSpecificationFactory specFactory;

        public ProductCategoryCommandFactory(IProductCategoryQueryRepository repository, IProductCategoryRepositoryListSpecificationFactory specFactory)
        {
            this.repository = repository;
            this.specFactory = specFactory;
        }

        public IProductCategoryQuery NewQuery()
        {
            return new ProductCategoryQuery(repository, specFactory);
        }
    }
}
