using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.ShoppingCart.Base.Application
{
    public interface IShoppingCartApplicationService<T> where T: ISerializable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>
        void List(Action<IList<T>> success, Action<Exception> error);
    }
}
