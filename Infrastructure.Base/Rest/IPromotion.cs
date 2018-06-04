using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Rest
{
    public interface IPromotion
    {
        /// <summary>
        /// 
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        long Category_Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IList<IPromotionPolice> Policies { get; set; }
    }
}
