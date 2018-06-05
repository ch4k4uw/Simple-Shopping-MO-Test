using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace FastShopping.Droid.ShoppingCart.Adapter
{
    public class ShoppingCartViewHolder : RecyclerView.ViewHolder
    {
        public readonly ImageView Img;
        public readonly TextView Name;
        public readonly TextView Quantity;
        public readonly TextView Discount;
        public readonly TextView Price;

        public ShoppingCartViewHolder(View itemView) : base(itemView)
        {
            Img = itemView.FindViewById<ImageView>(Resource.Id.img);
            Name = itemView.FindViewById<TextView>(Resource.Id.name);
            Quantity = itemView.FindViewById<TextView>(Resource.Id.quantity);
            Discount = itemView.FindViewById<TextView>(Resource.Id.discount);
            Price = itemView.FindViewById<TextView>(Resource.Id.price);
        }

        protected ShoppingCartViewHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
}