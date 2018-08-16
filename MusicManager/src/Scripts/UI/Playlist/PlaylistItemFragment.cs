using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;


namespace MusicManager
{
    public class PlaylistItemFragment : Fragment
    {
        public static event System.Action<string> OnEditButtonClick;
        public static event Action<string> OnFragmentClick;

        Button editButton;
        TextView titleTextView;
        ImageView itemImage;
        LinearLayout rootLayout;

        MediaFile music;
        View currentView;
        bool isWasInit = false;
        string musicID;

        public PlaylistItemFragment()
        {
        }

        public PlaylistItemFragment(PlaylistItemFragment fragment)
        {
            isWasInit = fragment.isWasInit;
            itemImage = fragment.itemImage;
            titleTextView = fragment.titleTextView;
            editButton = fragment.editButton;
            music = fragment.music;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            currentView = inflater.Inflate(Resource.Layout.fragment_playlist_item, container, false);
            musicID = Arguments.GetString(PlaylistManager.BUNDLE_MUSIC_KEY, "");

            if (!isWasInit)
            {
                AsyncInit();
            }

            isWasInit = true;

            return currentView;
        }

        public override void OnDestroyView()
        {
            editButton.Click -= EditButton_Click;
            rootLayout.Click -= RootLayout_Click;

            base.OnDestroyView();
        }

        private async Task AsyncInit()
        {
            await InitializeItemAsync();
        }

        private async Task InitializeItemAsync()
        {
            await Task.Run(() =>
            {
                editButton = currentView.FindViewById<Button>(Resource.Id.playlist_edit_item_button);
                titleTextView = currentView.FindViewById<TextView>(Resource.Id.playlist_item_title);
                itemImage = currentView.FindViewById<ImageView>(Resource.Id.playlist_item_image);
                rootLayout = currentView.FindViewById<LinearLayout>(Resource.Id.playlist_item_root);

                editButton.Click += EditButton_Click;
                rootLayout.Click += RootLayout_Click;

                music = PlaylistManager.Instance.AllMusicItems[musicID].Music;
                if (music != null)
                {
                    titleTextView.Text = music.Metadata.DisplayTitle;
                    itemImage.SetImageURI(Android.Net.Uri.Parse(music.Metadata.AlbumArtUri));
                }
                else
                {
                    titleTextView.SetText("Here is an error", TextView.BufferType.Normal);
                }

                return true;
            });
        }

        private void RootLayout_Click(object sender, EventArgs e)
        {
            OnFragmentClick?.Invoke(musicID);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            OnEditButtonClick?.Invoke(musicID);
        }

        
    }
}