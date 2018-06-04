using Domain.Base.Repository;
using Domain.Common.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Base.Repository
{
    public interface IProductCategoryQueryRepository: ILongIdQueryRepository<IProductCategoryEntity>
    {
    }
}
