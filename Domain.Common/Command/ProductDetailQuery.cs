using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Command;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Command
{
    internal class ProductDetailQuery : IProductDetailQuery
    {
        private readonly long id;
        private readonly IShoppingRepository shoppingRepository;
        private readonly IShoppingQueryRepositorySpecificationFactory shoppingRepositorySpecFactory;
        private readonly IProductQueryRepository repository;
        private readonly IProductQueryByIdRepositorySpecificationFactory factory;

        public ProductDetailQuery(long id, IShoppingRepository shoppingRepository, IShoppingQueryRepositorySpecificationFactory shoppingRepositorySpecFactory, IProductQueryRepository repository, IProductQueryByIdRepositorySpecificationFactory factory)
        {
            this.id = id;
            this.shoppingRepository = shoppingRepository;
            this.shoppingRepositorySpecFactory = shoppingRepositorySpecFactory;
            this.repository = repository;
            this.factory = factory;
        }

        public void Exec(Action<IProductAggregation> success, Action<Exception> error)
        {
            shoppingRepository.Find(shoppingRepositorySpecFactory.NewActiveShoppingSpec(), shoppings =>
            {
                if(shoppings.Count > 0 && !shoppings[0].IsFinished)
                {
                    repository.GetById(factory.NewQuerySpec(shoppings[0], id), success, error);
                }
                else
                {
                    repository.GetById(factory.NewQuerySpec(id), success, error);
                }
            }, error);
        }
    }
}
