using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Exception;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.ProductCatalog.Base.Command;
using Domain.ProductCatalog.Base.Factory.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Command
{
    public class ProductCatalogQuery : IProductCatalogQuery
    {
        private readonly IProductQueryRepository repository;
        private readonly IProductQueryRepositorySpecificationFactory specFactory;
        private readonly IShoppingRepository shoppingRepository;
        private readonly IShoppingQueryRepositorySpecificationFactory shoppingRepositorySpecFactory;
        public ProductCatalogQuery(IProductQueryRepository repository, IProductQueryRepositorySpecificationFactory specFactory, IShoppingRepository shoppingRepository, IShoppingQueryRepositorySpecificationFactory shoppingRepositorySpecFactory)
        {
            this.repository = repository;
            this.specFactory = specFactory;
            this.shoppingRepository = shoppingRepository;
            this.shoppingRepositorySpecFactory = shoppingRepositorySpecFactory;
        }

        public void Exec(Action<IList<IProductAggregation>> success, Action<Exception> error)
        {
            shoppingRepository.Find(shoppingRepositorySpecFactory.NewActiveShoppingSpec(), shoppings =>
            {
                if (shoppings.Count == 0 || shoppings[0].IsFinished)
                {
                    error(new NoActiveShoppingException());
                }
                else
                {
                    repository.Find(specFactory.NewCatalogSpec(shoppings[0].Id), success, error);
                }
            }, error);
        }
    }
}
