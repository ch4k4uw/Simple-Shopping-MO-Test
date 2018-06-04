using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.ProductCatalog.Base.Application
{
    public interface IInitializeShoppingApplicationService
    {
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="complete"></param>
        void Initialize(Action complete);
    }
}
