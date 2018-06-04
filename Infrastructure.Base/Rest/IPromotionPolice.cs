using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Rest
{
    public interface IPromotionPolice
    {
        /// <summary>
        /// 
        /// </summary>
        int Min { get; set; }

        /// <summary>
        /// 
        /// </summary>
        float Discount { get; set; }
    }
}
