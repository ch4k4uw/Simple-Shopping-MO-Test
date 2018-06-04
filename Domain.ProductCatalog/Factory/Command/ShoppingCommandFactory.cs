using Domain.Common.Base.Factory.Entity;
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
    public class ShoppingCommandFactory : IShoppingCommandFactory
    {
        private readonly IEntityFactory entityFactory;
        private readonly IShoppingRepository shoppingRepository;
        private readonly IShoppingQueryRepositorySpecificationFactory specificationFactory;
        private readonly IShoppingRepositoryByIdSpecificationFactory byIdSpecificationFactory;

        public ShoppingCommandFactory(IEntityFactory entityFactory, IShoppingRepository shoppingRepository, IShoppingQueryRepositorySpecificationFactory specificationFactory, IShoppingRepositoryByIdSpecificationFactory byIdSpecificationFactory)
        {
            this.entityFactory = entityFactory;
            this.shoppingRepository = shoppingRepository;
            this.specificationFactory = specificationFactory;
            this.byIdSpecificationFactory = byIdSpecificationFactory;
        }

        public IFinalizeShoppingCommand NewFinalizeShoppingCommand()
        {
            return new FinalizeShoppingCommand(shoppingRepository, specificationFactory, byIdSpecificationFactory);
        }

        public IInitializeShoppingCommand NewInitShoppingCommand()
        {
            return new InitializeShoppingCommand(shoppingRepository, specificationFactory, entityFactory);
        }
    }
}
