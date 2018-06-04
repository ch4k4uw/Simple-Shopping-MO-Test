using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.ProductCatalog.Base.Repository.Specification;

namespace Infrastructure.Base.Repository.Specification
{
    internal class ShoppingRepositoryByIdSpecification: IShoppingRepositoryByIdSpecification
    {
        public ShoppingRepositoryByIdSpecification(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}