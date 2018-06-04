using Domain.Base.Factory.Specification;
using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Factory.Specification
{
    public interface IProductQueryByIdRepositorySpecificationFactory: IByLongIdRepositorySpecificationFactory<IProductAggregation>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IProductRepositoryByIdSpecification NewQuerySpec(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingEntity"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        IProductRepositoryByIdSpecification NewQuerySpec(IShoppingEntity shoppingEntity, long productId);
    }
}
