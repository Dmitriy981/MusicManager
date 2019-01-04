using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.Design.Widget;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MusicManager
{
    class GUIManager : Singleton<GUIManager>
    {
        public BottomNavigationView NavigationView { get; set; }

        public Activity CurrentActivity { get; set; }

        public static ArrayAdapter<string> ArrayAdapterForList(List<string> list, string hint)
        {
            list.Insert(0, hint);

            ArrayAdapterWithHint result = new ArrayAdapterWithHint(Instance.CurrentActivity.ApplicationContext,
                Resource.Layout.support_simple_spinner_dropdown_item,
                list);
            result.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);

            return result;
        }
    }
}