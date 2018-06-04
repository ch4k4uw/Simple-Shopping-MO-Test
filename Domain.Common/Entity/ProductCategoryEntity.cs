using Domain.Common.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Entity
{
    internal class ProductCategoryEntity : IProductCategoryEntity
    {
        private static IProductCategoryEntity empty = null;
        public static IProductCategoryEntity Empty
        {
            get
            {
                if (empty == null)
                {
                    empty = new ProductCategoryEntity();
                }
                return empty;
            }
        }

        public ProductCategoryEntity(): this(0, "")
        {

        }

        public ProductCategoryEntity(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set; }

        public long Id { get; private set; }
    }
}
