using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Command
{
    public interface IListQuery<T, TId> where T : IEntity<TId>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void Exec(Action<IList<T>> success, Action<Exception> error);
    }
}
