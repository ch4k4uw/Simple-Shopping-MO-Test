using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Command
{
    public interface IQuery<T, TId> where T : IEntity<TId>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void Exec(Action<T> success, Action<Exception> error);
    }
}
