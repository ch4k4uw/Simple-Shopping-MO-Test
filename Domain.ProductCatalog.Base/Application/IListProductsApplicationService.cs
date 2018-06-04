using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.ProductCatalog.Base.Application
{
    public interface IListProductsApplicationService<T> where T: ISerializable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void List(Action<IList<T>> success, Action<Exception> error);

    }
}
