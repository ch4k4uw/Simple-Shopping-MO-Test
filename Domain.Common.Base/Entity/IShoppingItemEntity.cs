using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Entity
{
    public interface IShoppingItemEntity: ILongIdEntity
    {
        /// <summary>
        /// 
        /// </summary>
        long ShoppingId { get; }

        /// <summary>
        /// 
        /// </summary>
        int Quantity { get; }

        /// <summary>
        /// 
        /// </summary>
        double Price { get; }

        /// <summary>
        /// 
        /// </summary>
        float Discount { get; }
        
    }
}
