using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Repository
{
    public interface IShoppingItemRepository: IShoppingItemCommandRepository, IShoppingItemQueryRepository
    {
    }
}
