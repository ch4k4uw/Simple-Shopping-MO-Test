using Domain.Common.Base.Value;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Value
{
    internal class ProductPromotionDetailValue : IProductPromotionDetailValue
    {
        private readonly int min;
        private readonly float discount;

        private static IProductPromotionDetailValue empty;
        public static IProductPromotionDetailValue Empty
        {
            get
            {
                if(empty == null)
                {
                    empty = new ProductPromotionDetailValue();
                }
                return empty;
            }
        }

        public ProductPromotionDetailValue(): this(0, 0)
        {
        }

        public ProductPromotionDetailValue(int min, float discount)
        {
            this.min = min;
            this.discount = discount;
        }

        public int Min => this.min;

        public float Discount => this.discount;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
