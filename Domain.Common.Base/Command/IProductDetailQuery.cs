using Domain.Base.Command;
using Domain.Common.Base.Aggregation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Command
{
    public interface IProductDetailQuery: ILongIdQuery<IProductAggregation>
    {
    }
}
