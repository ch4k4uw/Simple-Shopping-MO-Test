using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Entity
{
    public interface IShoppingEntity: ILongIdEntity
    {
        /// <summary>
        /// 
        /// </summary>
        DateTime Creation { get; }

        /// <summary>
        /// 
        /// </summary>
        DateTime Finished { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsFinished { get;  }

        /// <summary>
        /// 
        /// </summary>
        void Finish();
    }
}
