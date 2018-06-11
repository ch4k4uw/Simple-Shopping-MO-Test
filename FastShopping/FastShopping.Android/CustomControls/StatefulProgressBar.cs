using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using FastShopping.Droid.CustomControls.Base.State;
using Java.Interop;
using Java.Lang;

namespace FastShopping.Droid.CustomControls
{
    public class StatefulProgressBar : ProgressBar
    {
        public StatefulProgressBar(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public StatefulProgressBar(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        public override IParcelable OnSaveInstanceState()
        {
            var ss = base.OnSaveInstanceState();

            var savedState = new SavedState()
            {
                Ss = ss,
                Visibility = (int)Visibility
            };

            return savedState;
        }

        public override void OnRestoreInstanceState(IParcelable state)
        {
            if(!(state is SavedState))
            {
                base.OnRestoreInstanceState(state);
            }
            else
            {
                var savedState = state as SavedState;
                base.OnRestoreInstanceState(savedState.Ss);

                Visibility = (ViewStates)savedState.Visibility;
            }
        }

        private void Initialize()
        {
        }

        private class SavedState : Java.Lang.Object, IParcelable
        {
            public int Visibility;
            public IParcelable Ss;

            [ExportField("CREATOR")]
            public static StatefulControlParcelableCreator<SavedState> InitializeCreator()
            {
                Console.WriteLine("MyParcelable.InitializeCreator");
                return new StatefulControlParcelableCreator<SavedState>(parcel => new SavedState(parcel));
            }

            public SavedState()
            {
            }

            public SavedState(Parcel source)
            {
                Visibility = source.ReadInt();
                Ss = source.ReadBundle();
            }

            public int DescribeContents()
            {
                Console.WriteLine("MyParcelable.DescribeContents");
                return 0;
            }

            public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
            {
                Console.WriteLine("MyParcelable.WriteToParcel");
                dest.WriteInt(Visibility);
                dest.WriteParcelable(Ss, ParcelableWriteFlags.None);
            }
        }
    }
}