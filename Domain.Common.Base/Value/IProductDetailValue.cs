using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Value
{
    public interface IProductDetailValue: ICloneable
    {
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 
        /// </summary>
        string Photo { get; }

        /// <summary>
        /// 
        /// </summary>
        double Price { get; }

        /// <summary>
        /// 
        /// </summary>
        long CategoryId { get; }

    }
}
