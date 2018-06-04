using Application.Dto.Result;
using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Assembler
{
    internal class Assembler
    {
        public static ProductDetail FromProductAggregation(IProductAggregation product)
        {
            return new ProductDetail
            {
                Name = product.Detail.Name,
                ProductCategory = new ProductCategory
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name
                },
                Description = product.Detail.Description,
                Discount = product.Discount,
                Photo = product.Detail.Photo,
                Price = product.Detail.Price,
                Quantity = product.Quantity
            };
        }

        public static IList<ProductListItem> FromProductAggregations(IList<IProductAggregation> products)
        {
            return products.Select(x => new ProductListItem
            {
                Id = x.Id,
                Category = new ProductCategory
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                Name = x.Detail.Name,
                Discount = x.Discount,
                Photo = x.Detail.Photo,
                Price = x.Detail.Price,
                Quantity = x.Quantity
            }).ToList();
        }

        public static IList<ProductCategory> FromProductCategories(IList<IProductCategoryEntity> categories)
        {
            return categories.Select(x => new ProductCategory
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
