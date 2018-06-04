using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Entity
{
    internal class ShoppingItemEntity : IShoppingItemEntity
    {
        private readonly long shoppingId;
        private readonly int quantity;
        private readonly double price;
        private readonly float discount;

        private static IShoppingItemEntity empty = null;
        public static IShoppingItemEntity Empty
        {
            get
            {
                if (empty == null)
                {
                    empty = new ShoppingItemEntity();
                }
                return empty;
            }
        }

        public ShoppingItemEntity(): this(null, null)
        {
        }

        public ShoppingItemEntity(IShoppingEntity shoppingEntity, IProductAggregation productAggregation)
        {
            Id = productAggregation?.Id ?? 0;
            shoppingId = shoppingEntity?.Id ?? 0;
            quantity = productAggregation?.Quantity ?? 0;
            price = productAggregation?.Detail.Price ?? 0;
            discount = productAggregation?.Discount ?? 0;
        }

        public long Id { get; internal set; }

        public long ShoppingId => shoppingId;

        public int Quantity => quantity;

        public double Price => price;

        public float Discount => discount;
    }
}
