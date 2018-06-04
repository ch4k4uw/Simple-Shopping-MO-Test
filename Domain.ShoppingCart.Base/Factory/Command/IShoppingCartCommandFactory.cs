using Domain.Base.Factory.Command;
using Domain.Common.Base.Aggregation;
using Domain.ShoppingCart.Base.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ShoppingCart.Base.Factory.Command
{
    public interface IShoppingCartCommandFactory: ILongIdCommandFactory<IProductAggregation>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IShoppingCartQuery NewQuery();
    }
}
