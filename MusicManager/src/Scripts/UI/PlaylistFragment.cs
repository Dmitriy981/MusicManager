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
    public class PlaylistFragment : ScreenFragment
    {
        LinearLayout scrollRoot;
        EditPopup editPopup;

        string lastClickedMusic = "";


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            SetView(Resource.Layout.fragment_playlist, inflater, container);
            CurrentScreenType = ScreenType.Playlist;

            PlaylistManager.Instance.CreatePlaylist(this.FragmentManager);
            editPopup = new EditPopup(_currentView.Context);

            PlaylistItemFragment.OnEditButtonClick += PlaylistItemFragment_OnEditButtonClick;
            PlaylistItemFragment.OnFragmentClick += PlaylistItemFragment_OnFragmentClick;

            return _currentView;
        }

        public override void OnDestroyView()
        {
            PlaylistItemFragment.OnEditButtonClick -= PlaylistItemFragment_OnEditButtonClick;
            PlaylistItemFragment.OnFragmentClick -= PlaylistItemFragment_OnFragmentClick;

            base.OnDestroyView();
        }

        private void PlaylistItemFragment_OnEditButtonClick(string musicName)
        {
            editPopup.SetContentView(Resource.Layout.popup_edit_music);
            editPopup.Initialize(MusicManager.Instance.MusicByName(musicName));
            editPopup.Show();
        }

        private void PlaylistItemFragment_OnFragmentClick(string musicName)
        {
            MediaFile clickedMusic = MusicManager.Instance.MusicByName(musicName);
            if (lastClickedMusic != musicName)
            {
                PlayerManager.Instance.PlayMusic(clickedMusic);
            }
            else
            {
                PlayerManager.Instance.PlayButtonClick();
            }

            lastClickedMusic = musicName;
        }
    }
}