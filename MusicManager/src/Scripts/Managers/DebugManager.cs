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
    class DebugManager : Singleton<DebugManager>
    {
        public static EditText debugArea;

        public static void Log(string log)
        {
#if DEBUG
            if (debugArea != null)
            {
                debugArea.Text += "\nDebug<" + CurrentDate() + ">: " + log;
            }
#endif
        }

        public static string CurrentDate()
        {
            return DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond;
        }
    }
}