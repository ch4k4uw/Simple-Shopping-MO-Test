using Application.Dto.Result;
using Domain.ShoppingCart.Base.Application;
using Domain.ShoppingCart.Base.Factory.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ShoppingCartService : IShoppingCartApplicationService<ProductListItem>
    {
        private readonly IShoppingCartCommandFactory commandFactory;
        public ShoppingCartService(IShoppingCartCommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public void List(Action<IList<ProductListItem>> success, Action<Exception> error)
        {
            commandFactory.NewQuery()
                .Exec(products => 
                {
                    success.Invoke(Assembler.Assembler.FromProductAggregations(products));
                }, error);
        }
    }
}
