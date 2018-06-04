using Domain.Base.Command;
using Domain.Common.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Base.Command
{
    public interface IProductCategoryQuery: ILongIdListQuery<IProductCategoryEntity>
    {
    }
}
