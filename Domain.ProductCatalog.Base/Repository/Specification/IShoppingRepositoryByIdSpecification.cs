using Domain.Base.Repository.Specification;
using Domain.Common.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Base.Repository.Specification
{
    public interface IShoppingRepositoryByIdSpecification: IByLongIdRepositorySpecification<IShoppingEntity>
    {
    }
}
