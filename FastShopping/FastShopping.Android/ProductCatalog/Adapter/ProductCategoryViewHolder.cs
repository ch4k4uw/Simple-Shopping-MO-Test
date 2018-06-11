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
    public class ProductCategoryViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {
        public readonly TextView Name;
        private readonly Action<int> itemClick;

        public ProductCategoryViewHolder(View itemView, Action<int> itemClick) : base(itemView)
        {
            Name = itemView as TextView;
            Name.SetOnClickListener(this);
            this.itemClick = itemClick;
        }

        protected ProductCategoryViewHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public void OnClick(View v)
        {
            itemClick.Invoke(AdapterPosition);
        }
    }
}