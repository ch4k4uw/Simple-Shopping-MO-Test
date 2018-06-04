using Domain.Base.Repository;
using Domain.Common.Base.Aggregation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Repository
{
    public interface IProductQueryRepository: ILongIdQueryRepository<IProductAggregation>
    {
    }
}
