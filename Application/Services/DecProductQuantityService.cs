using Application.Dto.Result;
using Domain.Common.Base.Application.Service;
using Domain.Common.Base.Factory.Command;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class DecProductQuantityService : IDecProductQuantityApplicationService<ProductDetail>
    {
        private readonly IProductDetailCommandFactory commandFactory;
        private readonly IShoppingRepository shoppingRepository;
        private readonly IShoppingQueryRepositorySpecificationFactory shoppingRepositorySpecFactory;
        private readonly IShoppingItemRepository shoppingItemRepository;
        private readonly IShoppingItemByIdRepositorySpecificationFactory shoppingItemRepositorySpecFactory;

        public DecProductQuantityService(IProductDetailCommandFactory commandFactory, IShoppingRepository shoppingRepository, IShoppingQueryRepositorySpecificationFactory shoppingRepositorySpecFactory, IShoppingItemRepository shoppingItemRepository, IShoppingItemByIdRepositorySpecificationFactory shoppingItemRepositorySpecFactory)
        {
            this.commandFactory = commandFactory;
            this.shoppingRepository = shoppingRepository;
            this.shoppingRepositorySpecFactory = shoppingRepositorySpecFactory;
            this.shoppingItemRepository = shoppingItemRepository;
            this.shoppingItemRepositorySpecFactory = shoppingItemRepositorySpecFactory;
        }

        public void Dec(long id, Action<ProductDetail> success, Action<Exception> error)
        {
            commandFactory.NewQuery(id).Exec(product =>
            {
                product.DecQuantity(shoppingRepository, shoppingRepositorySpecFactory, shoppingItemRepository, shoppingItemRepositorySpecFactory, decResult =>
                {
                    success.Invoke(Assembler.Assembler.FromProductAggregation(product));
                }, error);
            }, error);
        }
    }
}
