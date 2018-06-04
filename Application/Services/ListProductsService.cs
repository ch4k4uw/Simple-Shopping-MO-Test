using Application.Dto.Result;
using Domain.ProductCatalog.Base.Application;
using Domain.ProductCatalog.Base.Factory.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ListProductsService : IListProductsApplicationService<ProductListItem>
    {
        private readonly IProductCatalogCommandFactory factory;

        public ListProductsService(IProductCatalogCommandFactory factory)
        {
            this.factory = factory;
        }

        public void List(Action<IList<ProductListItem>> success, Action<Exception> error)
        {
            factory.NewQuery()
                .Exec(x => success.Invoke(Assembler.Assembler.FromProductAggregations(x)), error);
        }
    }
}
