using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Repository
{
    public interface ILongIdQueryRepository<T>: IQueryRepository<T, long> where T: ILongIdEntity
    {
    }
}
