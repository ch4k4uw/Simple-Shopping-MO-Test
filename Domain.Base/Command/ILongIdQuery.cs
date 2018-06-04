using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Command
{
    public interface ILongIdQuery<T>: IQuery<T, long> where T: ILongIdEntity
    {
    }
}
