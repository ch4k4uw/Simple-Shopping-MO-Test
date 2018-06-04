using Domain.Base.Factory.Specification;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Factory.Specification
{
    public interface IShoppingQueryRepositorySpecificationFactory: ILongIdRepositorySpecificationFactory<IShoppingEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IShoppingRepositorySpecification NewActiveShoppingSpec();
    }
}
