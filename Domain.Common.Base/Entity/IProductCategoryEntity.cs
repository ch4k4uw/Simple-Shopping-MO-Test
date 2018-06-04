using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Entity
{
    public interface IProductCategoryEntity: ILongIdEntity
    {
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }
    }
}
