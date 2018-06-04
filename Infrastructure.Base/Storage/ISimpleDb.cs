using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Storage
{
    public interface ISimpleDb
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopping"></param>
        void SetShopping(IShopping shopping);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        IShopping GetShoppingById(long id);

        /// <summary>
        /// List shoppings order by IShopping::Created desc
        /// </summary>
        /// <returns></returns>
        IList<IShopping> ListShoppings();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingId"></param>
        /// <param name="shoppingItem"></param>
        void SetShoppingItem(long shoppingId, IShoppingItem shoppingItem);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingId"></param>
        /// <param name="shoppingItem"></param>
        /// <returns></returns>
        IShoppingItem GetShoppingItemById(long shoppingId, long shoppingItem);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingId"></param>
        /// <returns></returns>
        IList<IShoppingItem> ListShoppingItems(long shoppingId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="favorite"></param>
        void SetFavorite(IFavorite favorite);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IFavorite GetFavorite(long id);

    }
}
