using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Rest
{
    public interface IProduct
    {
        /// <summary>
        /// 
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Photo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        double Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        long Category_Id { get; set; }
    }
}
