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

namespace MusicManager
{
    class HelperClass
    {
        private static Activity _activity;

        private static Activity Activity { get { return _activity ?? (_activity = GUIManager.Instance.CurrentActivity); } }

        public static string GetStringByKey(int key)
        {
            return Activity.ApplicationContext.Resources.GetString(key);
        }

        public static int GetIdByString(string key)
        {
            return Activity.Resources.GetIdentifier(key, "id", Activity.PackageName);
        }
    }
}