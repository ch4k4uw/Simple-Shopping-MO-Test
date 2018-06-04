using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Storage
{
    public interface IShoppingItem
    {
        /// <summary>
        /// 
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        long ShoppingId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        float Discount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool Favorite { get; set; }
    }
}
