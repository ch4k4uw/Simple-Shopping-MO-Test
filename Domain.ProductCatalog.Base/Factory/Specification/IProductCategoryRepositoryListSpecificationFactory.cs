using Domain.Base.Factory.Specification;
using Domain.Common.Base.Entity;
using Domain.ProductCatalog.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Base.Factory.Specification
{
    public interface IProductCategoryRepositoryListSpecificationFactory: ILongIdRepositorySpecificationFactory<IProductCategoryEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IProductCategoryRepositoryListSpecification NewListSpec();
    }
}
