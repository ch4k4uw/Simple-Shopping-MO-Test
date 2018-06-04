using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Exception;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.ShoppingCart.Base.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ShoppingCart.Command
{
    internal class ShoppingCartQuery : IShoppingCartQuery
    {
        private readonly IProductQueryRepository productRepository;
        private readonly IProductQueryRepositorySpecificationFactory productQueryRepositorySpecificationFactory;
        private readonly IShoppingRepository shoppingRepository;
        private readonly IShoppingQueryRepositorySpecificationFactory shoppingQueryRepositorySpecificationFactory;
        public ShoppingCartQuery(IProductQueryRepository productRepository, IProductQueryRepositorySpecificationFactory productQueryRepositorySpecificationFactory, IShoppingRepository shoppingRepository, IShoppingQueryRepositorySpecificationFactory shoppingQueryRepositorySpecificationFactory)
        {
            this.productRepository = productRepository;
            this.productQueryRepositorySpecificationFactory = productQueryRepositorySpecificationFactory;
            this.shoppingRepository = shoppingRepository;
            this.shoppingQueryRepositorySpecificationFactory = shoppingQueryRepositorySpecificationFactory;
        }

        public void Exec(Action<IList<IProductAggregation>> success, Action<Exception> error)
        {
            shoppingRepository.Find(shoppingQueryRepositorySpecificationFactory.NewActiveShoppingSpec(), shoppings => 
            {
                if(shoppings.Count == 0 || shoppings[0].IsFinished)
                {
                    error.Invoke(new NoActiveShoppingException());
                }
                else
                {
                    productRepository.Find(productQueryRepositorySpecificationFactory.NewCartSpec(shoppings[0].Id), success, error);
                }
            }, error);
        }
    }
}
