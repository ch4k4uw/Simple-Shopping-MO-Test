using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.ShoppingCart.Base.Command;
using Domain.ShoppingCart.Base.Factory.Command;
using Domain.ShoppingCart.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ShoppingCart.Factory.Command
{
    public class ShoppingCartCommandFactory : IShoppingCartCommandFactory
    {
        private readonly IProductQueryRepository productRepository;
        private readonly IProductQueryRepositorySpecificationFactory productQueryRepositorySpecificationFactory;
        private readonly IShoppingRepository shoppingRepository;
        private readonly IShoppingQueryRepositorySpecificationFactory shoppingQueryRepositorySpecificationFactory;

        public ShoppingCartCommandFactory(IProductQueryRepository productRepository, IProductQueryRepositorySpecificationFactory productQueryRepositorySpecificationFactory, IShoppingRepository shoppingRepository, IShoppingQueryRepositorySpecificationFactory shoppingQueryRepositorySpecificationFactory)
        {
            this.productRepository = productRepository;
            this.productQueryRepositorySpecificationFactory = productQueryRepositorySpecificationFactory;
            this.shoppingRepository = shoppingRepository;
            this.shoppingQueryRepositorySpecificationFactory = shoppingQueryRepositorySpecificationFactory;
        }

        public IShoppingCartQuery NewQuery()
        {
            return new ShoppingCartQuery(productRepository, productQueryRepositorySpecificationFactory, shoppingRepository, shoppingQueryRepositorySpecificationFactory);
        }
    }
}
