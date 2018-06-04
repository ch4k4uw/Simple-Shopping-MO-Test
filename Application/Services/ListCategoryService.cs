using Application.Dto.Result;
using Domain.ProductCatalog.Base.Application;
using Domain.ProductCatalog.Base.Factory.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ListCategoryService : IListCategoryApplicationService<ProductCategory>
    {
        private readonly IProductCategoryCommandFactory factory;
        public ListCategoryService(IProductCategoryCommandFactory factory)
        {
            this.factory = factory;
        }

        public void List(Action<IList<ProductCategory>> success, Action<Exception> error)
        {
            factory.NewQuery()
                .Exec(categories => success.Invoke(Assembler.Assembler.FromProductCategories(categories)), error);
        }
    }
}
