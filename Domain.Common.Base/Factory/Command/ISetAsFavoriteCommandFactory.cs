using Domain.Base.Factory.Command;
using Domain.Common.Base.Command;
using Domain.Common.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Factory.Command
{
    public interface ISetAsFavoriteCommandFactory: ILongIdCommandFactory<IFavoriteProductEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="favorite"></param>
        /// <returns></returns>
        ISetAsFavoriteCommand NewCommand(long id, bool favorite);
    }
}
