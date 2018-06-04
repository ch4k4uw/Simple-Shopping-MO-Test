using Domain.Base.Command;
using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Factory.Command
{
    public interface ILongIdCommandFactory<T>: ICommandFactory<T, long> where T: ILongIdEntity
    {
    }
}
