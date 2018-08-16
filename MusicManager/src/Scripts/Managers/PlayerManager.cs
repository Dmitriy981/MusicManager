using System;
using System.Collections.Generic;
using Android.Media;
using System.IO;
using Android.Provider;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using Android.Content;
using Android.Database;
using System.Threading;
using Newtonsoft.Json;

namespace MusicManager
{
    public enum PlayingType
    {
        RepeatPlaylist  = 0,
        RepeatMusic     = 1,
        Normal          = 2
    }

    class PlayerManager : LoadSingleton<PlayerManager>
    {
        public static event Action<MediaFile> OnMusicChanged;
        public static event Action<int> OnPlayerUpdate;
        public static event Action<bool> OnMusicPlayingStateChange; //isPlaying

        MediaPlayer player;
        MediaFile currentFile;
        int currentPlayerIndex = 0;
        List<string> currentPlaylist;
        PlayingType currentPlayingType = PlayingType.Normal;

        #region Properties

        MediaPlayer Player { get { return player ?? (player = new MediaPlayer()); } }

        public List<string> CurrentPlaylist { get { return currentPlaylist ?? (currentPlaylist = new List<string>()); } }
        
        public int СurrentPlayerIndex
        {
            get { return currentPlayerIndex; }
            set { currentPlayerIndex = value; }
        }

        #endregion

        #region Lifecycle

        public void Initialize()
        {
            Player.Completion += Player_Completion;
            PlayerFragment.OnSeekBarClick += PlayerFragment_OnSeekBarClick;
            MainActivity.OnInitialize += MainActivity_OnInitialize;

            CreateUpdater();
        }

        private void MainActivity_OnInitialize(bool isSuccess)
        {
            if (isSuccess)
            {
                if (CurrentPlaylist.Count == 0)
                {
                    MusicManager.Instance.PlayPlaylist();
                }
                else
                {
                    PlayPlaylist(CurrentPlaylist, false);
                }
            }
        }

        public void Save()
        {
            SaveManager.Save(this);
        }

        #endregion

        #region MusicControl

        public void PrevButtonClick()
        {
            ChangeCurrentIndex(-1);
        }

        public void NextButtonClick()
        {
            ChangeCurrentIndex(1);
        }

        public void PlayButtonClick()
        {
            if (Player.IsPlaying) Player.Pause();
            else Player.Start();

            OnMusicPlayingStateChange?.Invoke(Player.IsPlaying);
        }

        public void PlayMusic(MediaFile file)
        {
            Player.Completion -= Player_Completion;
            currentFile = file;

            Player.Reset();
            Player.SetDataSource(path: file.Url);
            Player.Prepare();
            Player.Start();

            OnMusicChanged?.Invoke(file);
            OnMusicPlayingStateChange?.Invoke(true);

            Player.Completion += Player_Completion;
        }

        public void PlayPlaylist(List<string> playlist, bool isResetIndex = true)
        {
            СurrentPlayerIndex = isResetIndex ? 0 : СurrentPlayerIndex;
            currentPlaylist = new List<string>(playlist);
            PlayCurrentMusic();
        }

        void ChangeCurrentIndex(int delta)
        {
            if (СurrentPlayerIndex == CurrentPlaylist.Count - 1 && currentPlayingType == PlayingType.Normal)
            {
                return;
            }

            if (currentPlayingType == PlayingType.RepeatMusic)
            {
                PlayCurrentMusic();
                return;
            }

            СurrentPlayerIndex += delta;
            СurrentPlayerIndex = UtilityManager.Clamp(0, CurrentPlaylist.Count, СurrentPlayerIndex);
            PlayCurrentMusic();
        }

        private void PlayCurrentMusic()
        {
            PlayMusic(MusicManager.Instance.MusicByName(currentPlaylist[СurrentPlayerIndex]));
        }

        #endregion

        #region Other

        private void CreateUpdater()
        {
            Thread updaterThread = new Thread(() =>
            {
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 1000;
                timer.Elapsed += UpdaterEvent;
                timer.Enabled = true;
            });

            updaterThread.Start();
        }

        #endregion

        #region Events

        private void Player_Completion(object sender, EventArgs e)
        {
            NextButtonClick();
        }

        private void PlayerFragment_OnSeekBarClick(int percent)
        {
            if (currentFile != null)
            {
                float clieckedMills = (percent / 100f) * currentFile.Metadata.Duration;
                Player.SeekTo((int)clieckedMills);
            }
        }

        private void UpdaterEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            int currentPercent = (int)((float)Player.CurrentPosition / currentFile.Metadata.Duration * 100f);
            OnPlayerUpdate?.Invoke(currentPercent);
        }

        #endregion
    }
}