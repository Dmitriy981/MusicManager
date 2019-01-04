using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MusicManager
{
    class ArrayAdapterWithHint : ArrayAdapter<string>
    {
        public ArrayAdapterWithHint(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public ArrayAdapterWithHint(Context context, int textViewResourceId, IList<string> objects) : base(context, textViewResourceId, objects)
        {
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            View view = base.GetDropDownView(position, convertView, parent);

            TextView tv = (TextView)view;

            if (position == 0)
            {
                tv.SetTextColor(Color.Gray);
            }
            else
            {
                tv.SetTextColor(Color.Black);
            }

            return view;
        }
    }
}