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
    class UtilityManager : Singleton<UtilityManager>
    {
        public static int Clamp(int min, int max, int value)
        {
            return Math.Min(Math.Max(value, min), max);
        }
        
        public static float Clamp(float min, float max, float value)
        {
            return Math.Min(Math.Max(value, min), max);
        }
    }
}