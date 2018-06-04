using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Value
{
    public interface IProductPromotionDetailValue: ICloneable
    {
        /// <summary>
        /// 
        /// </summary>
        int Min { get; }

        /// <summary>
        /// 
        /// </summary>
        float Discount { get; }

    }
}
