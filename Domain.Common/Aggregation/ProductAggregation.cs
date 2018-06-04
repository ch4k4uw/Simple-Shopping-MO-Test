using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Exception;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.Common.Base.Value;
using Domain.Common.Entity;
using Domain.Common.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Common.Aggregation
{
    internal class ProductAggregation : IProductAggregation
    {
        private readonly IProductDetailValue detail;
        private readonly IProductPromotionValue promotion;

        private static IProductAggregation empty = null;
        public static IProductAggregation Empty
        {
            get
            {
                if(empty == null)
                {
                    empty = new ProductAggregation();
                }
                return empty;
            }
        }

        public ProductAggregation(): this(0, null, null, null, 0, 0, false)
        {
        }

        public ProductAggregation(long id, IProductDetailValue detail, IProductPromotionValue promotion, IProductCategoryEntity productCategory, float discount, int quantity, bool favorite)
        {
            Id = id;
            this.detail = detail ?? ProductDetailValue.Empty;
            this.promotion = promotion ?? ProductPromotionValue.Empty;
            Category = productCategory ?? ProductCategoryEntity.Empty;
            Discount = discount;
            Quantity = quantity;
            IsFavorite = favorite;
        }

        public long Id { get; private set; }

        public IProductDetailValue Detail => detail.Clone() as IProductDetailValue;

        public IProductPromotionValue Promotion => promotion.Clone() as IProductPromotionValue;

        public float Discount { get; private set; }

        public int Quantity { get; private set; }

        public IProductCategoryEntity Category { get; private set; }

        public bool IsFavorite { get; private set; }

        public void DecQuantity(IShoppingRepository shoppingQueryRepository, IShoppingQueryRepositorySpecificationFactory shoppingQueryRepositorySpecificationFactory, IShoppingItemRepository shoppingItemRepository, IShoppingItemByIdRepositorySpecificationFactory shoppingItemByIdRepositorySpecificationFactory, Action<IProductAggregation> success, Action<Exception> error)
        {
            --Quantity;
            if (Quantity < 0)
            {
                Quantity = 0;
                Discount = 0;
            }
            else
            {
                UpdateDiscount();
            }

            Persist(shoppingQueryRepository, shoppingQueryRepositorySpecificationFactory, shoppingItemRepository, shoppingItemByIdRepositorySpecificationFactory, success, error);

        }

        public void IncQuantity(IShoppingRepository shoppingQueryRepository, IShoppingQueryRepositorySpecificationFactory shoppingQueryRepositorySpecificationFactory, IShoppingItemRepository shoppingItemRepository, IShoppingItemByIdRepositorySpecificationFactory shoppingItemByIdRepositorySpecificationFactory, Action<IProductAggregation> success, Action<Exception> error)
        {
            ++Quantity;
            UpdateDiscount();

            Persist(shoppingQueryRepository, shoppingQueryRepositorySpecificationFactory, shoppingItemRepository, shoppingItemByIdRepositorySpecificationFactory, success, error);

        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateDiscount()
        {
            Discount = 0;
            if (promotion.Details.Count > 0)
            {
                var details = promotion.Details.OrderBy(x => x.Min).ToList();
                foreach (var detail in details)
                {
                    if (Quantity >= detail.Min)
                    {
                        Discount = detail.Discount;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingQueryRepository"></param>
        /// <param name="shoppingQueryRepositorySpecificationFactory"></param>
        /// <param name="shoppingItemRepository"></param>
        /// <param name="shoppingItemByIdRepositorySpecificationFactory"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        private void Persist(IShoppingRepository shoppingQueryRepository, IShoppingQueryRepositorySpecificationFactory shoppingQueryRepositorySpecificationFactory, IShoppingItemRepository shoppingItemRepository, IShoppingItemByIdRepositorySpecificationFactory shoppingItemByIdRepositorySpecificationFactory, Action<IProductAggregation> success, Action<Exception> error)
        {
            shoppingQueryRepository.Find(shoppingQueryRepositorySpecificationFactory.NewActiveShoppingSpec(), shoppings =>
            {
                if(shoppings.Count == 0 || shoppings[0].IsFinished)
                {
                    error.Invoke(new NoActiveShoppingException());
                    return;
                }

                var shopping = shoppings[0];

                shoppingItemRepository.GetById(shoppingItemByIdRepositorySpecificationFactory.NewQuerySpec(shopping, Id), shoppingItem =>
                {
                    if (shoppingItem == ShoppingItemEntity.Empty)
                    {
                        shoppingItemRepository.Insert(new ShoppingItemEntity(shopping, this), id => {
                            success.Invoke(this);
                        }, err => {
                            error(err);
                        });
                    }
                    else
                    {
                        shoppingItemRepository.Update(new ShoppingItemEntity(shopping, this), shoppingItemByIdRepositorySpecificationFactory.NewUpdateSpec(shopping, Id), id =>
                        {
                            success.Invoke(this);
                        }, err =>
                        {
                            error(err);
                        });
                    }
                }, err =>
                {
                    error(err);
                });
            }, err =>
            {
                error(err);
            });
        }

    }
}
