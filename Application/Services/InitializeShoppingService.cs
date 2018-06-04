using Domain.ProductCatalog.Base.Application;
using Domain.ProductCatalog.Base.Factory.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class InitializeShoppingService : IInitializeShoppingApplicationService
    {
        private readonly IShoppingCommandFactory shoppingCommandFactory;

        public InitializeShoppingService(IShoppingCommandFactory shoppingCommandFactory)
        {
            this.shoppingCommandFactory = shoppingCommandFactory;
        }

        public void Initialize(Action complete)
        {
            shoppingCommandFactory.NewInitShoppingCommand()
                .Exec(complete);
        }
    }
}
