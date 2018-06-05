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
using Application.Dto.Result;

namespace FastShopping.Droid.ProductCatalog.Adapter
{
    public class ProductCategoryAdapter : RecyclerView.Adapter, View.IOnClickListener
    {
        private readonly Context context;
        private readonly IList<ProductCategory> list;
        private readonly Action<int> itemClick;

        public ProductCategoryAdapter(Context context, IList<ProductCategory> list, Action<int> itemClick)
        {
            this.context = context;
            this.list = list;
            this.itemClick = itemClick;
        }

        public override int ItemCount => list?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = list[position];
            var cHolder = holder as ProductCategoryViewHolder;
            cHolder.Name.Text = item.Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.category_filter_item, parent, false);
            return new ProductCategoryViewHolder(view, this);
        }

        public void OnClick(View v)
        {
            itemClick.Invoke((int)v.Tag);
        }
    }
}