using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Storage
{
    public interface IShopping
    {
        /// <summary>
        /// 
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime Created { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime Finished { get; set; }
    }
}
