using Domain.Base.Entity;
using Domain.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Repository
{
    public interface IQueryRepository<T, TId> where T: IEntity<TId>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="byIdRepositorySpecification"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void GetById(IByIdRepositorySpecification<T, TId> byIdRepositorySpecification, Action<T> success, Action<Exception> error);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositorySpecification"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void Find(IRepositorySpecification<T, TId> repositorySpecification, Action<IList<T>> success, Action<Exception> error);

    }
}
