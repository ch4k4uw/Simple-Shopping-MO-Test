using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Common.Base.Application.Service
{
    public interface IIncProductQuantityApplicationService<T> where T: ISerializable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void Inc(long id, Action<T> success, Action<System.Exception> error);
    }
}
