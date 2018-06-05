using Domain.Base.Repository.Specification;
using Domain.Common.Base.Aggregation;
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
    internal class ProductQueryRepository : Repository, IProductQueryRepository
    {
        public ProductQueryRepository(IScheduler uiThreadScheduler, ISimpleDb simpleDb, IRestApi restApi, IEntityFactory entityFactory, IValueFactory valueFactory): base(uiThreadScheduler, simpleDb, restApi, entityFactory, valueFactory)
        {
        }

        public void Find(IRepositorySpecification<IProductAggregation, long> repositorySpecification, Action<IList<IProductAggregation>> success, Action<Exception> error)
        {
            if (repositorySpecification is ProductRepositoryListCatalogSpecification || repositorySpecification is ProductRepositoryCartSpecification)
            {
                ReactiveFunction(() =>
                {
                    var isCatalog = repositorySpecification is ProductRepositoryListCatalogSpecification;
                    var shoppingId = isCatalog? (repositorySpecification as ProductRepositoryListCatalogSpecification).ShoppingId: (repositorySpecification as ProductRepositoryCartSpecification).ShoppingId;
                    
                    var shoppingItems = simpleDb.ListShoppingItems(shoppingId);
                    var products = restApi.ListProducts();
                    if (!isCatalog)
                    {
                        products = products.Join(shoppingItems.Where(x => x.Quantity > 0), product => product.Id, shoppingItem => shoppingItem.Id, (p, i) => p).ToList();
                    }

                    if(products?.Count > 0)
                    {
                        var categories = restApi.ListCategories();
                        var promotions = restApi.ListPromotions();

                        return products.Select(x => 
                        {
                            var shoppingItem = shoppingItems.FirstOrDefault(y => y.Id == x.Id);
                            var category = categories.FirstOrDefault(y => y.Id == x.Category_Id);
                            var favorite = simpleDb.GetFavorite(x.Id);
                            var promotion = restApi.ListPromotions().FirstOrDefault(y => y.Category_Id == (category?.Id ?? -1));

                            var productDetail = valueFactory.NewProductDetail(x.Name, x.Description, x.Photo, x.Price, x.Category_Id);
                            var categoryEntity = category != null ? entityFactory.NewProductCategory(category.Id, category.Name) : entityFactory.EmptyProductCategory();
                            var promotionDetailValues = promotion?.Policies.Select(y => valueFactory.NewProductPromotionDetail(y.Min, y.Discount)).ToList();
                            var promotionValue = valueFactory.NewProductPromotion(promotion?.Name ?? "", promotion?.Category_Id ?? 0, promotionDetailValues ?? new List<IProductPromotionDetailValue>());

                            return entityFactory.NewProduct(x.Id, productDetail, promotionValue, categoryEntity, shoppingItem?.Discount ?? 0, shoppingItem?.Quantity ?? 0, favorite.IsFavorite);

                        }).ToList();
                    }

                    return new List<IProductAggregation>();
                }
                , list =>
                {
                    success.Invoke(list);
                }, error);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void GetById(IByIdRepositorySpecification<IProductAggregation, long> byIdRepositorySpecification, Action<IProductAggregation> success, Action<Exception> error)
        {
            if(byIdRepositorySpecification is ProductRepositoryByIdSpecification || byIdRepositorySpecification is ShoppingProductRepositoryByIdSpecification)
            {
                ReactiveFunction(() =>
                {
                    var shoppingId = byIdRepositorySpecification is ShoppingProductRepositoryByIdSpecification? (byIdRepositorySpecification as ShoppingProductRepositoryByIdSpecification).ShoppingId: 0;
                    var productId = (byIdRepositorySpecification as ProductRepositoryByIdSpecification).Id;

                    var shoppingItem = simpleDb.GetShoppingItemById(shoppingId, productId);
                    var product = restApi.ListProducts().FirstOrDefault(x => x.Id == productId);
                    var category = restApi.ListCategories().FirstOrDefault(x => x.Id == (product?.Category_Id ?? -1));
                    var promotion = restApi.ListPromotions().FirstOrDefault(x => x.Category_Id == (category?.Id ?? -1));

                    if(product != null)
                    {
                        var productDetailValue = valueFactory.NewProductDetail(product.Name, product.Description, product.Photo, product.Price, product.Category_Id);
                        var promotionDetailValues = promotion?.Policies.Select(x => valueFactory.NewProductPromotionDetail(x.Min, x.Discount)).ToList();
                        var promotionValue = valueFactory.NewProductPromotion(promotion?.Name ?? "", promotion?.Category_Id ?? 0, promotionDetailValues ?? new List<IProductPromotionDetailValue>());
                        var categoryEntity = category != null ? entityFactory.NewProductCategory(category.Id, category.Name) : entityFactory.EmptyProductCategory();

                        return entityFactory.NewProduct(product.Id, productDetailValue, promotionValue, categoryEntity, shoppingItem?.Discount ?? 0, shoppingItem?.Quantity ?? 0, shoppingItem?.Favorite ?? false);

                    }

                    return entityFactory.EmptyProduct();
                }
                , item =>
                {
                    success.Invoke(item);
                }, error);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
