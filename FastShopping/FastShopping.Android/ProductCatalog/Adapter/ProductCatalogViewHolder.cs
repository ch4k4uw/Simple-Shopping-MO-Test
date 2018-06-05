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

namespace FastShopping.Droid.ProductCatalog.Adapter
{

    public class ProductCatalogViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnTouchListener
    {
        public readonly TextView Promotion;
        public readonly ImageView Img;
        public readonly TextView Name;
        public readonly TextView Discount;
        public readonly TextView Price;
        public readonly Button Add;
        public readonly Button Remove;
        public readonly TextView Quantity;
        public readonly RatingBar Favorite;
        private readonly View.IOnClickListener click;

        public ProductCatalogViewHolder(View itemView, View.IOnClickListener click) : base(itemView)
        {
            Promotion = itemView.FindViewById<TextView>(Resource.Id.promotion);
            Img = itemView.FindViewById<ImageView>(Resource.Id.img);
            Name = itemView.FindViewById<TextView>(Resource.Id.name);
            Discount = itemView.FindViewById<TextView>(Resource.Id.discount);
            Price = itemView.FindViewById<TextView>(Resource.Id.price);
            Add = itemView.FindViewById<Button>(Resource.Id.add);
            Remove = itemView.FindViewById<Button>(Resource.Id.remove);
            Quantity = itemView.FindViewById<TextView>(Resource.Id.quantity);
            Favorite = itemView.FindViewById<RatingBar>(Resource.Id.favorite);

            Add.SetOnClickListener(this);
            Remove.SetOnClickListener(this);
            Favorite.SetOnTouchListener(this);

            this.click = click;
        }

        protected ProductCatalogViewHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            buttonView.Tag = AdapterPosition;
            click.OnClick(buttonView);
        }

        public void OnClick(View v)
        {
            v.Tag = AdapterPosition;
            click.OnClick(v);
        }

        public void ChangeCheckState(bool isChecked)
        {
            Favorite.Rating = isChecked ? 1 : 0;
        }

        private float x1 = 0, x2 = 0, y1 = 0, y2 = 0;
        public bool OnTouch(View v, MotionEvent e)
        {
            switch(e.Action)
            {
                case MotionEventActions.Down:
                    x1 = e.GetX(0);
                    y1 = e.GetY(0);
                    break;
                case MotionEventActions.Up:
                    x2 = e.GetX(0);
                    y2 = e.GetY(0);

                    var xDiff = Math.Abs(x1 - x2);
                    var yDiff = Math.Abs(y1 - y2);
                    if(xDiff < 200 && yDiff < 200)
                    {
                        Favorite.Rating = Favorite.Rating == 1 ? 0 : 1;
                        OnClick(Favorite);
                    }
                    break;
            }
            return true;
        }
    }
}