using Domain.Common.Base.Value;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Value
{
    internal class ProductPromotionValue : IProductPromotionValue
    {
        private readonly string name;
        private readonly long categoryId;
        private readonly IList<IProductPromotionDetailValue> details;

        private static IProductPromotionValue empty = null;
        public static IProductPromotionValue Empty
        {
            get
            {
                if(empty == null)
                {
                    empty = new ProductPromotionValue();
                }
                return empty;
            }
        }

        public ProductPromotionValue(): this(null, 0, null)
        {
        }

        public ProductPromotionValue(string name, long categoryId, IList<IProductPromotionDetailValue> details)
        {
            this.name = name ?? "";
            this.categoryId = categoryId;
            this.details = details ?? new List<IProductPromotionDetailValue>();
        }

        public string Name => name;

        public long CategoryId => categoryId;

        public IList<IProductPromotionDetailValue> Details => details;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
