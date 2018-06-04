using Domain.Common.Base.Entity;
using Domain.Common.Base.Factory.Entity;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.ProductCatalog.Base.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Command
{
    public class InitializeShoppingCommand : IInitializeShoppingCommand
    {
        private readonly IShoppingRepository shoppingRepository;
        private readonly IShoppingQueryRepositorySpecificationFactory specificationFactory;
        private readonly IEntityFactory entityFactory;

        public InitializeShoppingCommand(IShoppingRepository shoppingRepository, IShoppingQueryRepositorySpecificationFactory specificationFactory, IEntityFactory entityFactory)
        {
            this.shoppingRepository = shoppingRepository;
            this.specificationFactory = specificationFactory;
            this.entityFactory = entityFactory;
        }

        public void Exec(Action complete)
        {
            shoppingRepository.Find(specificationFactory.NewActiveShoppingSpec(), shoppings =>
            {
                if (shoppings.Count == 0 || shoppings[0].IsFinished)
                {
                    shoppingRepository.Insert(entityFactory.NewShopping(), id => complete.Invoke(), err => complete.Invoke());
                }
                else
                {
                    complete.Invoke();
                }
            }, error => complete.Invoke());
        }
    }
}
