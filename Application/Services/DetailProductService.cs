using Application.Dto.Result;
using Domain.Common.Base.Application;
using Domain.Common.Base.Application.Service;
using Domain.Common.Base.Factory.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class DetailProductService : IDetailProductApplicationService<ProductDetail>
    {
        private readonly IProductDetailCommandFactory commandFactory;

        public DetailProductService(IProductDetailCommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public void Detail(long id, Action<ProductDetail> result)
        {
            commandFactory.NewQuery(id).Exec(product =>
            {
                result.Invoke(Assembler.Assembler.FromProductAggregation(product));
            }, err =>
            {
                result.Invoke(null);
            });
        }
    }
}
