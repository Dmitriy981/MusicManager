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
    }
}