using Domain.Common.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Repository.Specification
{
    internal class ShoppingProductRepositoryByIdSpecification: ProductRepositoryByIdSpecification
    {
        public ShoppingProductRepositoryByIdSpecification(long shoppingId, long productId): base(productId)
        {
            ShoppingId = shoppingId;
        }

        public long ShoppingId { get; }
    }
}
