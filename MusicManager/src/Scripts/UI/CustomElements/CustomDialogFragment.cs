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

namespace MusicManager
{
    public class CustomDialogFragment : DialogFragment
    {
        protected View _currentView;
        protected Action _createCallback;

        public Action CreateCallback { set { _createCallback = value; } }

        public void SetView(int id, LayoutInflater inflater, ViewGroup container)
        {
            _currentView = inflater.Inflate(id, container, false);
        }
    }
}