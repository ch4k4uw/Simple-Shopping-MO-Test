using Domain.Base.Factory.Specification;
using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Factory.Specification
{
    public interface IProductQueryRepositorySpecificationFactory: ILongIdRepositorySpecificationFactory<IProductAggregation>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingId"></param>
        /// <returns></returns>
        IProductRepositoryListSpecification NewCatalogSpec(long shoppingId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingId"></param>
        /// <returns></returns>
        IProductRepositoryListSpecification NewCartSpec(long shoppingId);
    }
}
