using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.IO;
using Android.Media;
using Android.Support.Design.Widget;
using Android.Graphics;

namespace MusicManager
{
    public class PlayerFragment : ScreenFragment
    {
        public static event System.Action<int> OnSeekBarClick;

        ImageView prev;
        ImageView play;
        ImageView next;
        FrameLayout playerLayout;
        SeekBar playerSeek;
        float navigationYPosition = 0;

        ImageView musicArt;
        TextView musicName;
        TextView musicArtist;
        ImageView musicBackgroundArt;


        public float NavigationYPosition
        {
            get
            {
                if ((int)navigationYPosition == 0)
                {
                    navigationYPosition = GUIManager.Instance.NavigationView.Top;
                }

                return navigationYPosition;
            }
        }

        #region lifecycle

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            SetView(Resource.Layout.fragment_player, inflater, container);
            CurrentScreenType = ScreenType.Player;

            prev = _currentView.FindViewById<ImageView>(Resource.Id.player_prev);
            prev.Click += Prev_Click;

            play = _currentView.FindViewById<ImageView>(Resource.Id.player_play);
            play.Click += Play_Click;

            next = _currentView.FindViewById<ImageView>(Resource.Id.player_next);
            next.Click += Next_Click;

            playerLayout = _currentView.FindViewById<FrameLayout>(Resource.Id.player_root_layout);
            playerLayout.Drag += PlayerLayout_Drag;
            playerLayout.Touch += PlayerLayout_Touch;

            playerSeek = _currentView.FindViewById<SeekBar>(Resource.Id.player_music_seekBar);
            playerSeek.ProgressChanged += PlayerSeek_ProgressChanged;
            playerSeek.StopTrackingTouch += PlayerSeek_StopTrackingTouch;

            navigationYPosition = GUIManager.Instance.NavigationView.Top;

            PlayerManager.OnMusicChanged += PlayerManager_OnMusicChanged;
            PlayerManager.OnPlayerUpdate += PlayerManager_OnPlayerUpdate;
            PlayerManager.OnMusicPlayingStateChange += PlayerManager_OnMusicPlayingStateChange;

            musicArt = _currentView.FindViewById<ImageView>(Resource.Id.player_music_image);
            musicName = _currentView.FindViewById<TextView>(Resource.Id.player_music_name);
            musicArtist = _currentView.FindViewById<TextView>(Resource.Id.player_music_artist);
            musicBackgroundArt = _currentView.FindViewById<ImageView>(Resource.Id.player_music_background_image);

            return _currentView;
        }

        public override void OnDestroyView()
        {
            PlayerManager.OnMusicChanged -= PlayerManager_OnMusicChanged;
            prev.Click -= Prev_Click;
            play.Click -= Play_Click;
            next.Click -= Next_Click;
            playerLayout.Drag -= PlayerLayout_Drag;
            playerLayout.Touch -= PlayerLayout_Touch;
            playerSeek.ProgressChanged -= PlayerSeek_ProgressChanged;
            playerSeek.StopTrackingTouch -= PlayerSeek_StopTrackingTouch;

            base.OnDestroyView();
        }

        #endregion


        #region Events
        
        private void PlayerManager_OnMusicChanged(MediaFile file)
        {
            Android.Net.Uri albumURL = Android.Net.Uri.Parse(file.Metadata.AlbumArtUri);
            musicName.SetText(file.Metadata.DisplayTitle, TextView.BufferType.Normal);
            musicArtist.SetText(file.Metadata.Artist, TextView.BufferType.Normal);
            musicArt.SetImageURI(albumURL);
            musicBackgroundArt.SetImageURI(albumURL);
            playerSeek.SetProgress(0, false);
        }

        private void PlayerLayout_Touch(object sender, View.TouchEventArgs e)
        {
            switch (e.Event.Action)
            {
                case MotionEventActions.Move:
                    ClipData dragData = ClipData.NewPlainText("", "");
                    View.DragShadowBuilder myShadow = new View.DragShadowBuilder(playerLayout);
                    playerLayout.StartDragAndDrop(dragData, myShadow, null, 0);
                    break;
            }
        }

        private void PlayerLayout_Drag(object sender, View.DragEventArgs e)
        {
            switch (e.Event.Action)
            {
                case DragAction.Ended:
                    playerLayout.TranslationY = UtilityManager.Clamp(0, NavigationYPosition - playerLayout.Height, e.Event.GetY());
                    break;
            }
        }

        private void PlayerSeek_StopTrackingTouch(object sender, SeekBar.StopTrackingTouchEventArgs e)
        {

        }

        private void PlayerSeek_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            if (e.FromUser)
            {
                OnSeekBarClick?.Invoke(e.Progress);
            }
        }

        private void PlayerManager_OnPlayerUpdate(int percent)
        {
            playerSeek.SetProgress(percent, true);
        }

        private void PlayerManager_OnMusicPlayingStateChange(bool isPLaying)
        {
            play.SetImageResource(isPLaying ? Resource.Drawable.pause : Resource.Drawable.play);
        }

        private void Next_Click(object sender, EventArgs e)
        {
            PlayerManager.Instance.NextButtonClick();
        }
        
        private void Play_Click(object sender, EventArgs e)
        {
            PlayerManager.Instance.PlayButtonClick();
        }
        
        private void Prev_Click(object sender, EventArgs e)
        {
            PlayerManager.Instance.PrevButtonClick();
        }

        #endregion
    }
}