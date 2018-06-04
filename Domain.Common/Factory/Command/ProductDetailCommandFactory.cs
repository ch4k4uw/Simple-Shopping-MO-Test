using Domain.Common.Base.Command;
using Domain.Common.Base.Factory.Command;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.Common.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Factory.Command
{
    public class ProductDetailCommandFactory : IProductDetailCommandFactory
    {
        private readonly IShoppingRepository shoppingRepository;
        private readonly IShoppingQueryRepositorySpecificationFactory shoppingRepositorySpecFactory;
        private readonly IProductQueryRepository repository;
        private readonly IProductQueryByIdRepositorySpecificationFactory factory;
        public ProductDetailCommandFactory(IShoppingRepository shoppingRepository, IShoppingQueryRepositorySpecificationFactory shoppingRepositorySpecFactory, IProductQueryRepository repository, IProductQueryByIdRepositorySpecificationFactory factory)
        {
            this.shoppingRepository = shoppingRepository;
            this.shoppingRepositorySpecFactory = shoppingRepositorySpecFactory;
            this.repository = repository;
            this.factory = factory;
        }

        public IProductDetailQuery NewQuery(long id)
        {
            return new ProductDetailQuery(id, shoppingRepository, shoppingRepositorySpecFactory, repository, factory);
        }
    }
}
