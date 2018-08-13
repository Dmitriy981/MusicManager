using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Support.V4.App;
using Android;
using System.Collections;
using System.Threading.Tasks;
using Android.Content.PM;
using Android.Runtime;
using Android.Content;
using Android.Database;
using System.Collections.Generic;

namespace MusicManager
{
    public enum ScreenType
    {
        None        = 0,
        Player      = 1,
        Playlist    = 2,
        Settings    = 3
    }

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        public static event System.Action<bool> OnInitialize;
        public static event System.Action<ScreenType> OnScreenChanged;
        Android.App.Fragment _playlistFragment;
        Android.App.Fragment _playerFragment;
        Android.App.Fragment _settingsFragment;

        bool _isInitialized = false;

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            ScreenType currentType = ScreenType.None;
            List<Android.App.Fragment> addFragments = new List<Android.App.Fragment>();
            //Window.RequestFeature(Window.FEAture)
            
            switch (item.ItemId)
            {
                case Resource.Id.navigation_playlist:
                    currentType = ScreenType.Playlist;
                    SetActionBarVisibility(true);
                    if (_playlistFragment == null)
                    {
                        _playlistFragment = new PlaylistFragment();
                        addFragments.Add(_playlistFragment);
                    }
                    break;
                case Resource.Id.navigation_player:
                    currentType = ScreenType.Player;
                    SetActionBarVisibility(false);
                    if (_playerFragment == null)
                    {
                        _playerFragment = new PlayerFragment();
                        addFragments.Add(_playerFragment);
                    }
                    break;
                case Resource.Id.navigation_settings:
                    currentType = ScreenType.Settings;
                    SetActionBarVisibility(true);
                    if (_settingsFragment == null)
                    {
                        _settingsFragment = new SettingsFragment();
                        addFragments.Add(_settingsFragment);
                    }
                    break;
            }

            if (addFragments.Count != 0)
            {
                foreach (Android.App.Fragment fragment in addFragments)
                {
                    Android.App.FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();

                    fragmentTx.Add(Resource.Id.fragments_root, fragment);
                    fragmentTx.AddToBackStack(null);
                    fragmentTx.Commit();
                }
            }

            OnScreenChanged?.Invoke(currentType);

            return true;
        }

        #region Lifecycle
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.activity_main);

            ActivityCompat.RequestPermissions(this,
               new string[] {
                       Manifest.Permission.ReadExternalStorage,
                       Manifest.Permission.WriteExternalStorage
               },
               200);

            base.OnCreate(savedInstanceState);
            //SetSupportActionBar(FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar));
            DebugManager.debugArea = FindViewById<EditText>(Resource.Id.debug_pane);
#if !DEBUG
            DebugManager.debugArea.Visibility = ViewStates.Gone;
#endif

            GUIManager.Instance.NavigationView = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            GUIManager.Instance.NavigationView.SetOnNavigationItemSelectedListener(this);
            GUIManager.Instance.NavigationView.SelectedItemId = Resource.Id.navigation_player;
            GUIManager.Instance.CurrentActivity = this;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.playlist_top_bar, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_search:
                    DebugManager.Log("serach");
                    break;

                case Resource.Id.action_play:
                    DebugManager.Log("play");
                    break;

                case Resource.Id.action_filter:
                    DebugManager.Log("filter");
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnPause()
        {
            if(_isInitialized)
            {
                SaveAll();
            }

            base.OnPause();
        }

        #endregion

        #region OnStart

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            bool isSuccessfully = requestCode == 200;

            if (isSuccessfully)
            {
                Initialize();
                _isInitialized = true;
            }

            OnInitialize?.Invoke(isSuccessfully);
        }
        
        void Initialize()
        {
            MusicManager.Instance.Initialize();
            PlayerManager.Instance.Initialize();
            PlaylistManager.Instance.Initialize();
            RatingManager.Instance.Initialize();
        }

        void SaveAll()
        {
            RatingManager.Instance.Save();
            PlayerManager.Instance.Save();
            MusicManager.Instance.Save();
        }

        #endregion


        #region Helper methods

        void SetActionBarVisibility(bool isVisible)
        {
            //Window.RequestFeature(WindowFeatures.ActionBar);
            if (ActionBar != null)
            {
                if (isVisible) ActionBar.Show();
                else ActionBar.Hide();
            }
        }

        #endregion
    }
}

