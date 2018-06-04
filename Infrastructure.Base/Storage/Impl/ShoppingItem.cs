using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Storage.Impl
{
    public class ShoppingItem : IShoppingItem
    {
        private readonly ISimpleDb db;
        private long id;
        private long shoppingId;
        private int quantity;
        private float discount;
        private bool favorite;

        public ShoppingItem(ISimpleDb db): this(db, 0, 0, 0, 0, false)
        {
        }

        public ShoppingItem(ISimpleDb db, long id, long shoppingId, int quantity, float discount, bool favorite)
        {
            this.db = db;
            this.id = id;
            this.shoppingId = shoppingId;
            this.quantity = quantity;
            this.discount = discount;
            this.favorite = favorite;
        }

        public long Id
        {
            get { return id; }
            set
            {
                id = value;
                if (id != 0)
                {
                    var favorite = db.GetFavorite(id);
                    if (favorite.Id == id)
                    {
                        this.favorite = favorite.IsFavorite;
                    }
                }
                else
                {
                    favorite = false;
                }
            }
        }

        public long ShoppingId
        {
            get { return shoppingId; }
            set
            {
                shoppingId = value;
            }
        }

        public int Quantity {
            get { return quantity; }
            set
            {
                quantity = value;
                Update();
            }
        }

        public float Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                Update();
            }
        }

        public bool Favorite
        {
            get { return favorite; }
            set
            {
                favorite = value;
                if (id != 0)
                {
                    var favorite = db.GetFavorite(id);
                    favorite.IsFavorite = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            if (id != 0 && shoppingId != 0)
            {
                db.SetShoppingItem(shoppingId, this);
            }
        }
    }
}
