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

namespace FastShopping.Droid.CustomControls.Base.State
{
    internal class StatefulControlParcelableCreator<T> : Java.Lang.Object, IParcelableCreator where T : Java.Lang.Object
    {
        private readonly Func<Parcel, T> objectCreator;

        public StatefulControlParcelableCreator(Func<Parcel, T> objectCreator)
        {
            this.objectCreator = objectCreator;
        }

        public Java.Lang.Object CreateFromParcel(Parcel source)
        {
            Console.WriteLine("MyParcelableCreator.CreateFromParcel");
            return objectCreator.Invoke(source);
        }

        public Java.Lang.Object[] NewArray(int size)
        {
            Console.WriteLine("MyParcelableCreator.NewArray");
            return new Java.Lang.Object[size];
        }
    }
}