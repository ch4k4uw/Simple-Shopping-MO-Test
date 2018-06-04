using Domain.Base.Repository;
using Domain.Common.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Repository
{
    public interface IShoppingItemCommandRepository: ILongIdCommandRepository<IShoppingItemEntity>
    {
    }
}
