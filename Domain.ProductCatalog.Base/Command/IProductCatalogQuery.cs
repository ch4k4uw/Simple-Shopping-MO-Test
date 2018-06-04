using Domain.Base.Command;
using Domain.Common.Base.Aggregation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Base.Command
{
    public interface IProductCatalogQuery: ILongIdListQuery<IProductAggregation>
    {
    }
}
