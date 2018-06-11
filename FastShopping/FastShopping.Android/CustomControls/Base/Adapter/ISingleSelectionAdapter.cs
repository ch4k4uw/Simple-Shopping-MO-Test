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

namespace FastShopping.Droid.CustomControls.Base.Adapter
{
    public interface ISingleSelectionAdapter
    {
        /// <summary>
        /// 
        /// </summary>
        int SelectedItem { get; set; }
    }
}