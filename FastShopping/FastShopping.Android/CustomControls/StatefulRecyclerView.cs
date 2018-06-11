using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using FastShopping.Droid.CustomControls.Base.Adapter;
using FastShopping.Droid.CustomControls.Base.State;
using Java.Interop;

namespace FastShopping.Droid.CustomControls
{

    public class StatefulRecyclerView: RecyclerView
    {
        private MySavedState pendingState;

        public StatefulRecyclerView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public StatefulRecyclerView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        protected override IParcelable OnSaveInstanceState()
        {
            var ss = base.OnSaveInstanceState();
            var adapter = GetAdapter();
            var selection = -1;
            if(adapter is ISingleSelectionAdapter)
            {
                selection = (adapter as ISingleSelectionAdapter).SelectedItem;
            }

            var savedState = new MySavedState()
            {
                Ss = ss,
                Selection = (int)selection
            };

            return savedState;
        }

        protected override void OnRestoreInstanceState(IParcelable state)
        {
            if (!(state is MySavedState))
            {
                base.OnRestoreInstanceState(state);
            }
            else
            {
                var savedState = state as MySavedState;
                base.OnRestoreInstanceState(savedState.Ss);

                if(GetAdapter() != null)
                {
                    var adapter = GetAdapter();
                    if(adapter is ISingleSelectionAdapter)
                    {
                        (adapter as ISingleSelectionAdapter).SelectedItem = savedState.Selection;
                    }
                }
                else
                {
                    pendingState = savedState;
                }
            }
        }

        public override void SetAdapter(Adapter adapter)
        {
            base.SetAdapter(adapter);
            if(pendingState != null && adapter is ISingleSelectionAdapter)
            {
                (adapter as ISingleSelectionAdapter).SelectedItem = pendingState.Selection;
            }
            pendingState = null;

        }

        private void Initialize()
        {
        }

        private class MySavedState : Java.Lang.Object, IParcelable
        {
            public int Selection;
            public IParcelable Ss;

            [ExportField("CREATOR")]
            public static StatefulControlParcelableCreator<MySavedState> InitializeCreator()
            {
                Console.WriteLine("MyParcelable.InitializeCreator");
                return new StatefulControlParcelableCreator<MySavedState>(parcel => new MySavedState(parcel));
            }

            public MySavedState()
            {
            }

            public MySavedState(Parcel source)
            {
                Selection = source.ReadInt();
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
                dest.WriteInt(Selection);
                dest.WriteParcelable(Ss, ParcelableWriteFlags.None);
            }
        }
    }
}