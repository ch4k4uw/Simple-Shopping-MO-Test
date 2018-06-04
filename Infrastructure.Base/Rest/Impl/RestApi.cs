using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Base.Rest.Impl
{
    internal class ProductDto : IProduct
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public double Price { get ; set; }
        public long Category_Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    internal class PromotionPoliceDto : IPromotionPolice, ICloneable
    {
        public int Min { get; set; }
        public float Discount { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    internal class PromotionDto: IPromotion
    {
        public string Name { get; set; }
        public long Category_Id { get; set; }
        public IList<IPromotionPolice> Policies { get; set; }
    }

    internal class RestPromotion: ICloneable
    {
        public string Name { get; set; }
        public long Category_Id { get; set; }
        public List<PromotionPoliceDto> Policies { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    internal class CategoryDto : ICategory, ICloneable
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }



    internal class CacheItem<T> 
    {
        private IList<T> data;
        private DateTime? expire;
        public CacheItem()
        {
            data = null;
            expire = null;
        }

        public IList<T> Data
        {
            get
            {
                if(expire == null || expire?.CompareTo(DateTime.Now) < 0)
                {
                    data = null;
                }

                return data;
            }
            set
            {
                expire = DateTime.Now.AddMinutes(5);
                data = value;
            }
        }
    }

    public class RestApi : IRestApi
    {
        private RestClient client = null;

        private CacheItem<ProductDto> productCache;
        private CacheItem<RestPromotion> promotionCache;
        private CacheItem<CategoryDto> categoryCache;

        public IList<IProduct> ListProducts()
        {
            Init();
            var result = productCache.Data;
            if (result == null)
            {
                var request = new RestRequest("eVqp7pfX", Method.GET)
                {
                    OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
                };

                var response = client.Execute<List<ProductDto>>(request);

                result = response.Data;
                productCache.Data = result;
            }
            else
            {
                result = new List<ProductDto>(result.ToArray());
            }

            return new List<IProduct>(result.ToArray());
        }

        public IList<IPromotion> ListPromotions()
        {
            Init();
            var result = promotionCache.Data;
            if (result == null)
            {
                var request = new RestRequest("R9cJFBtG", Method.GET)
                {
                    OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
                };

                var response = client.Execute<List<RestPromotion>>(request);

                result = response.Data;
                promotionCache.Data = result;
            }
            else
            {
                result = new List<RestPromotion>(result.ToArray());
            }
            return new List<IPromotion>(result.Select(x => new PromotionDto
            {
                Name = x.Name,
                Category_Id = x.Category_Id,
                Policies = x.Policies != null? new List<IPromotionPolice>(x.Policies.ToArray()): null
            }).ToArray());
        }

        public IList<ICategory> ListCategories()
        {
            Init();
            var result = categoryCache.Data;
            if (result == null)
            {
                var request = new RestRequest("YNR2rsWe", Method.GET)
                {
                    OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
                };
            
                var response = client.Execute<List<CategoryDto>>(request);

                result = response.Data;
                categoryCache.Data = result;
            }
            else
            {
                result = new List<CategoryDto>(result.ToArray());
            }
            return new List<ICategory>(result.ToArray());
        }

        private void Init()
        {
            if(client == null)
            {
                client = new RestClient("https://pastebin.com/raw/");
                productCache = new CacheItem<ProductDto>();
                promotionCache = new CacheItem<RestPromotion>();
                categoryCache = new CacheItem<CategoryDto>();
            }
        }
    }
}
