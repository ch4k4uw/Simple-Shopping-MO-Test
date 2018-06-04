using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Base.Application
{
    public interface IFinalizeShoppingApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="complete"></param>
        void Finalyze(Action complete);
    }
}
