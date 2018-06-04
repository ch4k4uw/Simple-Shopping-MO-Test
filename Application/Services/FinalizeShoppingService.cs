using Domain.ProductCatalog.Base.Application;
using Domain.ProductCatalog.Base.Factory.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class FinalizeShoppingService : IFinalizeShoppingApplicationService
    {
        private readonly IShoppingCommandFactory shoppingCommandFactory;

        public FinalizeShoppingService(IShoppingCommandFactory shoppingCommandFactory)
        {
            this.shoppingCommandFactory = shoppingCommandFactory;
        }

        public void Finalyze(Action complete)
        {
            shoppingCommandFactory.NewFinalizeShoppingCommand()
                .Exec(complete);
        }
    }
}
