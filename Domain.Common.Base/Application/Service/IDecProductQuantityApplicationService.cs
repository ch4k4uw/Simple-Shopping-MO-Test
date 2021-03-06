﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Common.Base.Application.Service
{
    public interface IDecProductQuantityApplicationService<T> where T: ISerializable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void Dec(long id, Action<T> success, Action<System.Exception> error);
    }
}
