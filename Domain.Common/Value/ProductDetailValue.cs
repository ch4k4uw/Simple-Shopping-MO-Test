using Domain.Common.Base.Value;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Value
{
    internal class ProductDetailValue : IProductDetailValue
    {
        private readonly string name;
        private readonly string description;
        private readonly string photo;
        private readonly double price;
        private readonly long categoryId;

        private static IProductDetailValue empty = null;
        public static IProductDetailValue Empty
        {
            get
            {
                if(empty == null)
                {
                    empty = new ProductDetailValue();
                }
                return empty;
            }
        }

        internal ProductDetailValue() : this(null, null, null, 0, 0)
        {
        }

        internal ProductDetailValue(string name, string description, string photo, double price, long categoryId)
        {
            this.name = name ?? "";
            this.description = description ?? "";
            this.photo = photo ?? "";
            this.price = price;
            this.categoryId = categoryId;
        }

        public string Name =>
            name;

        public string Description => 
            description;

        public string Photo => 
            photo;

        public double Price => 
            price;

        public long CategoryId => 
            categoryId;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
