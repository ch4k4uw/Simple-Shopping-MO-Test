using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Repository.Specification
{
    public interface ILongIdRepositorySpecification<T>: IRepositorySpecification<T, long> where T: ILongIdEntity
    {
    }
}
