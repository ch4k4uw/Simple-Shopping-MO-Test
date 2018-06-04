using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Infrastructure.Base.Storage;
using Infrastructure.Base.Storage.Impl;
using Newtonsoft.Json;

namespace Infrastructure.Android.Storage
{
    internal class ShoppingDto : IShopping
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Finished { get; set; }
    }

    internal class ShoppingItemDto : IShoppingItem
    {
        public long Id { get; set; }
        public long ShoppingId { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        [JsonIgnore]
        public bool Favorite { get; set; }
    }

    internal class FavoriteDto : IFavorite
    {
        public long Id { get; set; }
        public bool IsFavorite { get; set; }
    }

    public class AndroidSimpleDb : ISimpleDb
    {
        private readonly Context context;

        private ISharedPreferences shoppings;
        private ISharedPreferences shoppingItems;
        private ISharedPreferences favorites;

        private bool started;


        public AndroidSimpleDb(Context context)
        {
            this.context = context;
            started = false;
        }

        public IFavorite GetFavorite(long id)
        {
            Init();
            var rawValue = favorites.GetString(BuildFavoriteKey(id), "");
            if (rawValue != "")
            {
                var value = JsonConvert.DeserializeObject<FavoriteDto>(rawValue);
                return new Favorite(this, value.Id, value.IsFavorite);
            }
            return new Favorite(this);
        }

        public IShopping GetShoppingById(long id)
        {
            Init();
            var rawValue = shoppings.GetString(BuildShoppingKey(id), "");
            if(rawValue != "")
            {
                var value = JsonConvert.DeserializeObject<ShoppingDto>(rawValue);
                return new Shopping(this, value.Id, value.Created, value.Finished);
            }
            return new Shopping(this);
        }

        public IShoppingItem GetShoppingItemById(long shoppingId, long shoppingItemId)
        {
            Init();
            var rawValue = shoppingItems.GetString(BuildShoppingItemKey(shoppingId), "");
            if (rawValue != "")
            {
                var rawValues = JsonConvert.DeserializeObject<IDictionary<string, string>>(rawValue);
                if(rawValues.Count > 0 && rawValues.ContainsKey(BuildShoppingItemSubKey(shoppingItemId)))
                {
                    var rawSubValue = rawValues[BuildShoppingItemSubKey(shoppingItemId)];
                    if(rawSubValue != null)
                    {
                        var value = JsonConvert.DeserializeObject<ShoppingItemDto>(rawSubValue);
                        var favorite = GetFavorite(value.Id);

                        return new ShoppingItem(this, value.Id, value.ShoppingId, value.Quantity, value.Discount, favorite.IsFavorite);

                    }
                }
            }
            return new ShoppingItem(this);
        }

        public IList<IShoppingItem> ListShoppingItems(long shoppingId)
        {
            Init();
            var rawValue = shoppingItems.GetString(BuildShoppingItemKey(shoppingId), "");
            if (rawValue != "")
            {
                var rawValues = JsonConvert.DeserializeObject<IDictionary<string, string>>(rawValue);
                if (rawValues.Count > 0)
                {
                    var result = new List<IShoppingItem>();
                    foreach(var rawSubValue in rawValues)
                    {
                        var value = JsonConvert.DeserializeObject<ShoppingItemDto>(rawSubValue.Value);
                        result.Add(new ShoppingItem(this, value.Id, shoppingId, value.Quantity, value.Discount, GetFavorite(value.Id).IsFavorite));
                    }

                    return result;
                }
            }
            return new List<IShoppingItem>();
        }

        public IList<IShopping> ListShoppings()
        {
            Init();
            var result = new List<IShopping>();
            foreach(var rawValue in shoppings.All)
            {
                var value = JsonConvert.DeserializeObject<ShoppingDto>(rawValue.Value.ToString());
                result.Add(new Shopping(this, value.Id, value.Created, value.Finished));
            }
            return result.OrderByDescending(x => x.Created.Ticks).ToList();
        }

        public void SetFavorite(IFavorite favorite)
        {
            Init();
            if(favorite.Id != 0)
            {
                favorites.Edit()
                    .PutString(BuildFavoriteKey(favorite.Id), JsonConvert.SerializeObject(favorite))
                    .Commit();
            }
        }

        public void SetShopping(IShopping shopping)
        {
            Init();

            long id = shopping.Id;
            if(id == 0)
            {
                var shoppings = ListShoppings().OrderByDescending(x => x.Id).ToList();
                if(shoppings.Count > 0)
                {
                    id = shoppings[0].Id + 1;
                }
                else
                {
                    id = 1;
                }
            }

            shopping.Id = id;

            shoppings.Edit()
                .PutString(BuildShoppingKey(id), JsonConvert.SerializeObject(shopping))
                .Commit();

        }

        public void SetShoppingItem(long shoppingId, IShoppingItem shoppingItem)
        {
            Init();

            if (shoppingItem.Id != 0) {
                var rawValues = shoppingItems.GetString(BuildShoppingItemKey(shoppingId), "");
                IDictionary<string, string> values;

                if (rawValues == "")
                {
                    values = new Dictionary<string, string>();
                }
                else
                {
                    values = JsonConvert.DeserializeObject<IDictionary<string, string>>(rawValues);
                }

                values[BuildShoppingItemSubKey(shoppingItem.Id)] = JsonConvert.SerializeObject(shoppingItem);

                shoppingItems.Edit()
                    .PutString(BuildShoppingItemKey(shoppingId), JsonConvert.SerializeObject(values))
                    .Commit();
            }
        }

        private void Init()
        {
            if(!started)
            {
                shoppings = context.GetSharedPreferences("shoppings", FileCreationMode.Private);
                shoppingItems = context.GetSharedPreferences("shopping_items", FileCreationMode.Private);
                favorites = context.GetSharedPreferences("favorites", FileCreationMode.Private);

                started = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string BuildFavoriteKey(long id)
        {
            return $"prod_{id}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingId"></param>
        /// <returns></returns>
        private string BuildShoppingItemKey(long shoppingId)
        {
            return $"shpi_{shoppingId}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string BuildShoppingItemSubKey(long id)
        {
            return $"i_{id}";
        }

        /// <summary>
        /// 
        /// </summary>
        private string BuildShoppingKey(long id)
        {
            return $"shp_{id}";
        }
    }
}