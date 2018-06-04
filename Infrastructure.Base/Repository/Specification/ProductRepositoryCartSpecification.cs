using Domain.Common.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Repository.Specification
{
    internal class ProductRepositoryCartSpecification : IProductRepositoryListSpecification
    {
        public ProductRepositoryCartSpecification(long shoppingId)
        {
            ShoppingId = shoppingId;
        }

        public long ShoppingId { get; }
    }
}
