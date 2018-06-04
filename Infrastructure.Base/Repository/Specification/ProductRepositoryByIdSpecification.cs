using Domain.Common.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Repository.Specification
{
    internal class ProductRepositoryByIdSpecification: IProductRepositoryByIdSpecification
    {
        public ProductRepositoryByIdSpecification(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
