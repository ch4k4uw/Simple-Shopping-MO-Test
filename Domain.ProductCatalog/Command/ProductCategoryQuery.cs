using Domain.Common.Base.Entity;
using Domain.ProductCatalog.Base.Command;
using Domain.ProductCatalog.Base.Factory.Specification;
using Domain.ProductCatalog.Base.Repository;
using System;
using System.Collections.Generic;

namespace Domain.ProductCatalog.Command
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private IProductCategoryQueryRepository repository;
        private IProductCategoryRepositoryListSpecificationFactory specFactory;

        public ProductCategoryQuery(IProductCategoryQueryRepository repository, IProductCategoryRepositoryListSpecificationFactory specFactory)
        {
            this.repository = repository;
            this.specFactory = specFactory;
        }

        public void Exec(Action<IList<IProductCategoryEntity>> success, Action<Exception> error)
        {
            repository.Find(specFactory.NewListSpec(), success, error);
        }
    }
}
