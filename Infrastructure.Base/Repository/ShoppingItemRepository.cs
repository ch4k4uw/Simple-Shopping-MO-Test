using Domain.Base.Repository.Specification;
using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Factory.Entity;
using Domain.Common.Base.Factory.Value;
using Domain.Common.Base.Repository;
using Domain.Common.Base.Value;
using Infrastructure.Base.Repository.Specification;
using Infrastructure.Base.Rest;
using Infrastructure.Base.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;

namespace Infrastructure.Base.Repository
{
    internal class ShoppingItem : IShoppingItem
    {
        public long Id { get; set; }
        public long ShoppingId { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public bool Favorite { get; set; }
    }

    internal class ShoppingItemRepository : Repository, IShoppingItemRepository
    {
        public ShoppingItemRepository(IScheduler uiThreadScheduler, ISimpleDb simpleDb, IRestApi restApi, IEntityFactory entityFactory, IValueFactory valueFactory): base(uiThreadScheduler, simpleDb, restApi, entityFactory, valueFactory)
        {
        }

        public void Find(IRepositorySpecification<IShoppingItemEntity, long> repositorySpecification, Action<IList<IShoppingItemEntity>> success, Action<Exception> error)
        {
            throw new NotImplementedException();
        }

        public void GetById(IByIdRepositorySpecification<IShoppingItemEntity, long> byIdRepositorySpecification, Action<IShoppingItemEntity> success, Action<Exception> error)
        {
            if(byIdRepositorySpecification is ShoppingItemQueryByIdRepositorySpecification)
            {
                ReactiveFunction(() =>
                {
                    var shoppingId = (byIdRepositorySpecification as ShoppingItemQueryByIdRepositorySpecification).ShoppingId;
                    var itemId = (byIdRepositorySpecification as ShoppingItemQueryByIdRepositorySpecification).Id;

                    return ProcessByIdRequest(shoppingId, itemId, null);
                }
                , success, error);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void Insert(IShoppingItemEntity entity, Action<long> success, Action<Exception> error)
        {
            ReactiveFunction(() =>
            {
                var shoppingItem = new ShoppingItem
                {
                    Id = entity.Id,
                    Discount = entity.Discount,
                    Favorite = false,
                    Quantity = entity.Quantity,
                    ShoppingId = entity.ShoppingId
                };

                simpleDb.SetShoppingItem(entity.ShoppingId, shoppingItem);

                return shoppingItem.Id;
            }, success, error);
        }

        public void Update(IShoppingItemEntity entity, IByIdRepositorySpecification<IShoppingItemEntity, long> spec, Action<IShoppingItemEntity> success, Action<Exception> error)
        {
            if (spec is ShoppingItemUpdateByIdRepositorySpecification)
            {
                ReactiveFunction(() =>
                {
                    var shoppingId = (spec as ShoppingItemUpdateByIdRepositorySpecification).ShoppingId;
                    var itemId = (spec as ShoppingItemUpdateByIdRepositorySpecification).Id;

                    return ProcessByIdRequest(shoppingId, itemId, (shopping, shoppingItem, product, productDetailValue, promotionValue) => 
                    {
                        shoppingItem.Quantity = entity.Quantity;
                        shoppingItem.Discount = entity.Discount;

                        simpleDb.SetShoppingItem(shoppingId, shoppingItem);
                    });
                }
                , success, error);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingId"></param>
        /// <param name="itemId"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        private IShoppingItemEntity ProcessByIdRequest(long shoppingId, long itemId, Action<IShopping, IShoppingItem, IProduct, IProductDetailValue, IProductPromotionValue> callback)
        {
            var shopping = simpleDb.GetShoppingById(shoppingId);

            if (shopping.Id == shoppingId)
            {
                var shoppingItem = simpleDb.GetShoppingItemById(shoppingId, itemId);
                if (shoppingItem.Id == itemId)
                {
                    var product = restApi.ListProducts().FirstOrDefault(x => x.Id == itemId);
                    var category = restApi.ListCategories().FirstOrDefault(x => x.Id == (product?.Category_Id ?? -1));
                    var promotion = restApi.ListPromotions().FirstOrDefault(x => x.Category_Id == (category?.Id ?? -1));

                    var productDetailValue = valueFactory.NewProductDetail(product.Name, product.Description, product.Photo, product.Price, product.Category_Id);
                    var promotionDetailValues = promotion?.Policies.Select(x => valueFactory.NewProductPromotionDetail(x.Min, x.Discount)).ToList();
                    var promotionValue = valueFactory.NewProductPromotion(promotion?.Name ?? "", promotion?.Category_Id ?? 0, promotionDetailValues ?? new List<IProductPromotionDetailValue>());
                    var categoryEntity = category != null ? entityFactory.NewProductCategory(category.Id, category.Name) : entityFactory.EmptyProductCategory();

                    callback?.Invoke(shopping, shoppingItem, product, productDetailValue, promotionValue);

                    return entityFactory
                        .NewShoppingItem(
                            entityFactory.NewShopping(shopping.Id, shopping.Created, shopping.Finished),
                            entityFactory.NewProduct(product.Id, productDetailValue, promotionValue, categoryEntity, shoppingItem?.Discount ?? 0, shoppingItem?.Quantity ?? 0, shoppingItem?.Favorite ?? false)
                        );
                }
            }
            return entityFactory.EmptyShoppingItem();
        }
    }
}
