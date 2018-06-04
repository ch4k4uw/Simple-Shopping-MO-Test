using Domain.Common.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Repository.Specification
{
    internal class ShoppingItemUpdateByIdRepositorySpecification : IShoppingItemByIdRepositorySpecification
    {
        public ShoppingItemUpdateByIdRepositorySpecification(long shoppingId, long id)
        {
            ShoppingId = shoppingId;
            Id = id;
        }

        public long ShoppingId { get; }
        public long Id { get; }
    }
}
