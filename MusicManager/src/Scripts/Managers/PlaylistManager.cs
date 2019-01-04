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
    class PlaylistManager : Singleton<PlaylistManager>
    {
        public const string BUNDLE_MUSIC_KEY = "music_key";

        Dictionary<string, PlaylistItem> allMusicItems;

        public Dictionary<string, PlaylistItem> AllMusicItems
        {
            get
            {
                if (allMusicItems == null)
                {
                    allMusicItems = new Dictionary<string, PlaylistItem>();
                }

                return allMusicItems;
            }
        }


        public void Initialize()
        {
            GetAllMusics();
        }

        private void GetAllMusics()
        {
            foreach (KeyValuePair<string, MediaFile> pair in MusicManager.Instance.AllMusics)
            {
                PlaylistItemFragment fragment = new PlaylistItemFragment();

                PlaylistItem item = new PlaylistItem(pair.Value, fragment);

                AllMusicItems.Add(pair.Key, item);
            }
        }

        public FragmentManager FragmentManager { get; set; }
        public void CreatePlaylist(FragmentManager fragmentManager)
        {
            FragmentManager = fragmentManager;
            FragmentTransaction fragmentTx = fragmentManager.BeginTransaction();

            foreach (KeyValuePair<string, PlaylistItem> pair in AllMusicItems)
            {
                Bundle bundle = new Bundle();
                bundle.PutString(BUNDLE_MUSIC_KEY, pair.Key);

                PlaylistItemFragment newFragment = new PlaylistItemFragment(pair.Value.Fragment);
                newFragment.Arguments = bundle;

                fragmentTx.Add(Resource.Id.playlist_scroll_root, newFragment);
            }
            
            fragmentTx.AddToBackStack(null);
            fragmentTx.Commit();
        }
    }
}