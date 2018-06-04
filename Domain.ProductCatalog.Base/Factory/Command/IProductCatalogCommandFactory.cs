using Domain.Base.Factory.Command;
using Domain.Common.Base.Aggregation;
using Domain.ProductCatalog.Base.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Base.Factory.Command
{
    public interface IProductCatalogCommandFactory: ILongIdCommandFactory<IProductAggregation>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IProductCatalogQuery NewQuery();
    }
}
