using Domain.Base.Factory.Specification;
using Domain.Common.Base.Entity;
using Domain.ProductCatalog.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Base.Factory.Specification
{
    public interface IShoppingRepositoryByIdSpecificationFactory: IByLongIdRepositorySpecificationFactory<IShoppingEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IShoppingRepositoryByIdSpecification NewUpdateSpec(long id);
    }
}
