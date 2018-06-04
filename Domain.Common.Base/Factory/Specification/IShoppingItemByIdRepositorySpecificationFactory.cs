using Domain.Base.Factory.Specification;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Factory.Specification
{
    public interface IShoppingItemByIdRepositorySpecificationFactory: IByLongIdRepositorySpecificationFactory<IShoppingItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingEntity"></param>
        /// <param name="shoppingItemId"></param>
        /// <returns></returns>
        IShoppingItemByIdRepositorySpecification NewQuerySpec(IShoppingEntity shoppingEntity, long shoppingItemId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingEntity"></param>
        /// <param name="shoppingItemId"></param>
        /// <returns></returns>
        IShoppingItemByIdRepositorySpecification NewUpdateSpec(IShoppingEntity shoppingEntity, long shoppingItemId);


    }
}
