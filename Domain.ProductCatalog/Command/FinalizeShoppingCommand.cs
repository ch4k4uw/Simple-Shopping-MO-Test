using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.Common.Base.Repository.Specification;
using Domain.ProductCatalog.Base.Command;
using Domain.ProductCatalog.Base.Factory.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Command
{
    public class FinalizeShoppingCommand : IFinalizeShoppingCommand
    {
        private readonly IShoppingRepository shoppingRepository;
        private readonly IShoppingQueryRepositorySpecificationFactory specificationFactory;
        private readonly IShoppingRepositoryByIdSpecificationFactory byIdSpecificationFactory;

        public FinalizeShoppingCommand(IShoppingRepository shoppingRepository, IShoppingQueryRepositorySpecificationFactory specificationFactory, IShoppingRepositoryByIdSpecificationFactory byIdSpecificationFactory)
        {
            this.shoppingRepository = shoppingRepository;
            this.specificationFactory = specificationFactory;
            this.byIdSpecificationFactory = byIdSpecificationFactory;
        }

        public void Exec(Action complete)
        {
            shoppingRepository.Find(specificationFactory.NewActiveShoppingSpec(), shoppings => 
            {
                if(shoppings.Count == 0 || shoppings[0].IsFinished)
                {
                    complete.Invoke();
                }
                else
                {
                    shoppings[0].Finish();
                    shoppingRepository.Update(shoppings[0], byIdSpecificationFactory.NewUpdateSpec(shoppings[0].Id), shopping => complete.Invoke(), err => complete.Invoke());
                }
            }, error => complete.Invoke());
        }
    }
}
