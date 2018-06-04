using Domain.Common.Base.Factory.Value;
using Domain.Common.Base.Value;
using Domain.Common.Value;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Factory.Value
{
    public class ValueFactory : IValueFactory
    {
        public IProductDetailValue NewProductDetail(string name, string description, string photo, double price, long categoryId)
        {
            return new ProductDetailValue(name, description, photo, price, categoryId);
        }

        public IProductPromotionDetailValue NewProductPromotionDetail(int min, float discount)
        {
            return new ProductPromotionDetailValue(min, discount);
        }

        public IProductPromotionValue EmptyProductPromotion()
        {
            return ProductPromotionValue.Empty;
        }

        public IProductPromotionValue NewProductPromotion(string name, long categoryId, IList<IProductPromotionDetailValue> details)
        {
            return new ProductPromotionValue(name, categoryId, details);
        }

    }
}
