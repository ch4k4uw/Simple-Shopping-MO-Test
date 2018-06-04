using Domain.Base.Command;
using Domain.Common.Base.Aggregation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ShoppingCart.Base.Command
{
    public interface IShoppingCartQuery: ILongIdListQuery<IProductAggregation>
    {
    }
}
