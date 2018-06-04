using Domain.Common.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Repository.Specification
{
    internal class ShoppingItemQueryByIdRepositorySpecification: IShoppingItemByIdRepositorySpecification
    {
        public ShoppingItemQueryByIdRepositorySpecification(long shoppingId, long id)
        {
            ShoppingId = shoppingId;
            Id = id;
        }

        public long ShoppingId { get; }
        public long Id { get; }
    }
}
