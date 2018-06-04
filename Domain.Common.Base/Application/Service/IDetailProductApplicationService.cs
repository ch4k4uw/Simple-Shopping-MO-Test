using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Common.Base.Application.Service
{
    public interface IDetailProductApplicationService<T> where T: ISerializable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        void Detail(long id, Action<T> result);
    }
}
