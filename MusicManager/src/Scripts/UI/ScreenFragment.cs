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
   public class ScreenFragment : CustomFragment
    {
        ScreenType _currentScreenType = ScreenType.None;

        public ScreenType CurrentScreenType { set { _currentScreenType = value; } }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            MainActivity.OnScreenChanged += MainActivity_OnScreenChanged;

            base.OnViewCreated(view, savedInstanceState);
        }

        public override void OnDestroyView()
        {
            MainActivity.OnScreenChanged -= MainActivity_OnScreenChanged;

            base.OnDestroyView();
        }
        
        private void MainActivity_OnScreenChanged(ScreenType newType)
        {
            _currentView.Visibility = _currentScreenType == newType ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}