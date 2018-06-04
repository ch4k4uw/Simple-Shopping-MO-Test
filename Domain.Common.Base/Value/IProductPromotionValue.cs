using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Value
{
    public interface IProductPromotionValue: ICloneable
    {
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        long CategoryId { get; }

        /// <summary>
        /// 
        /// </summary>
        IList<IProductPromotionDetailValue> Details { get; }

    }
}
