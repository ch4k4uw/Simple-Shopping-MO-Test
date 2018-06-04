using Domain.Common.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Repository.Specification
{
    internal class ProductRepositoryListCatalogSpecification: IProductRepositoryListSpecification
    {
        public ProductRepositoryListCatalogSpecification(long shoppingId)
        {
            ShoppingId = shoppingId;
        }

        public long ShoppingId { get; }
    }
}
