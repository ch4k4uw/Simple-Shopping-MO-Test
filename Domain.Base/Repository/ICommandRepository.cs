using Domain.Base.Entity;
using Domain.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Repository
{
    public interface ICommandRepository<T, TId> where T: IEntity<TId>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void Insert(T entity, Action<TId> success, Action<Exception> error);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="spec"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void Update(T entity, IByIdRepositorySpecification<T, TId> spec, Action<T> success, Action<Exception> error);
    }
}
