using Domain.Base.Repository.Specification;
using Domain.Common.Base.Aggregation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Repository.Specification
{
    public interface IProductRepositoryListSpecification: ILongIdRepositorySpecification<IProductAggregation>
    {
    }
}
