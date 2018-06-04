using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.ProductCatalog.Base.Command;
using Domain.ProductCatalog.Base.Factory.Command;
using Domain.ProductCatalog.Base.Factory.Specification;
using Domain.ProductCatalog.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Factory.Command
{
    public class ProductCatalogCommandFactory : IProductCatalogCommandFactory
    {
        private readonly IProductQueryRepository repository;
        private readonly IProductQueryRepositorySpecificationFactory specFactory;
        private readonly IShoppingRepository shoppingRepository;
        private readonly IShoppingQueryRepositorySpecificationFactory shoppingRepositorySpecFactory;
        public ProductCatalogCommandFactory(IProductQueryRepository repository, IProductQueryRepositorySpecificationFactory specFactory, IShoppingRepository shoppingRepository, IShoppingQueryRepositorySpecificationFactory shoppingRepositorySpecFactory)
        {
            this.repository = repository;
            this.specFactory = specFactory;
            this.shoppingRepository = shoppingRepository;
            this.shoppingRepositorySpecFactory = shoppingRepositorySpecFactory;
        }

        public IProductCatalogQuery NewQuery()
        {
            return new ProductCatalogQuery(repository, specFactory, shoppingRepository, shoppingRepositorySpecFactory);
        }
    }
}
