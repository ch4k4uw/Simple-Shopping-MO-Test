using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Rest
{
    public interface ICategory
    {
        /// <summary>
        /// 
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; set; }
    }
}
