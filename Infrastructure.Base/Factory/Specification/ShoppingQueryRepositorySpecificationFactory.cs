using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository.Specification;
using Infrastructure.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Factory.Specification
{
    internal class ShoppingQueryRepositorySpecificationFactory : IShoppingQueryRepositorySpecificationFactory
    {
        public IShoppingRepositorySpecification NewActiveShoppingSpec()
        {
            return new ShoppingRepositorySpecification();
        }
    }
}
