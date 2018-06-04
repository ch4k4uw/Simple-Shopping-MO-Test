using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Repository
{
    public interface ILongIdCommandRepository<T>: ICommandRepository<T, long> where T: ILongIdEntity
    {
    }
}
