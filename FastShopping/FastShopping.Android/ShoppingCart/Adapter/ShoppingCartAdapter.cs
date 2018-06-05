using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Application.Dto.Result;
using FastShopping.Droid.Base.Shopping;

namespace FastShopping.Droid.ShoppingCart.Adapter
{
    public class ShoppingCartAdapter: ShoppingAdapter
    {
        private readonly IList<ProductListItem> list;
        public ShoppingCartAdapter(Context context, IList<ProductListItem> list) : base(context)
        {
            this.list = list;
        }

        public override int ItemCount => list?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = list[position];
            var pcHolder = holder as ShoppingCartViewHolder;

            pcHolder.Discount.Visibility = item.Discount == 0 ? ViewStates.Gone : ViewStates.Visible;
            pcHolder.Name.Text = item.Name;
            pcHolder.Discount.Text = item.Discount.ToString("F1", CultureInfo.InvariantCulture) + "%";
            pcHolder.Price.Text = "R$ " + (item.Quantity * (item.Price * (item.Discount > 0 ? 1.0f - (item.Discount / 100.0) : 1))).ToString("F2", CultureInfo.InvariantCulture);
            pcHolder.Quantity.Text = $"{item.Quantity}";

            DownloadImage(pcHolder.Img, item.Photo);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.shopping_cart_item, parent, false);
            return new ShoppingCartViewHolder(view);
        }
    }
}