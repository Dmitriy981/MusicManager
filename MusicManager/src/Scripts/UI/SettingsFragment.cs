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
    public class SettingsFragment : ScreenFragment
    {
        LinearLayout categoriesRoot;
        LinearLayout tagsRoot;

        #region Lifecycle

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            SetView(Resource.Layout.fragment_settings, inflater, container);
            CurrentScreenType = ScreenType.Settings;

            categoriesRoot = _currentView.FindViewById<LinearLayout>(Resource.Id.settings_categories_root);
            tagsRoot = _currentView.FindViewById<LinearLayout>(Resource.Id.settings_tags_root);

            FragmentTransaction fragmentTx = FragmentManager.BeginTransaction();

            SettingsCategoriesRootFragment categories = new SettingsCategoriesRootFragment();
            fragmentTx.Replace(Resource.Id.settings_fragment_categories_root, categories);

            SettingsTagsRootFragment tags = new SettingsTagsRootFragment();
            fragmentTx.Replace(Resource.Id.settings_fragment_tags_root, tags);

            fragmentTx.AddToBackStack(null);
            fragmentTx.Commit();

            return _currentView;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
        }

        #endregion
    }
}