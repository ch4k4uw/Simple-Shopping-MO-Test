using Domain.Base.Factory.Command;
using Domain.Common.Base.Entity;
using Domain.ProductCatalog.Base.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Base.Factory.Command
{
    public interface IShoppingCommandFactory: ILongIdCommandFactory<IShoppingEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IInitializeShoppingCommand NewInitShoppingCommand();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IFinalizeShoppingCommand NewFinalizeShoppingCommand();
    }
}
