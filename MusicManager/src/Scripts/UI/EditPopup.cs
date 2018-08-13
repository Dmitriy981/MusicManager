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
    class EditPopup : Dialog
    {
        TextView titleText;
        TextView closeTextButton;
        MediaFile editMusic;

        public EditPopup(Context context) : base(context) { }

        public void Initialize(MediaFile music)
        {
            editMusic = music;
            titleText = FindViewById<TextView>(Resource.Id.edit_popup_title);
            titleText.Text = music.Metadata.DisplayTitle;
        }

        public override void Dismiss()
        {
            closeTextButton.Click -= CloseTextButton_Click;

            base.Dismiss();
        }

        public override void Show()
        {
            base.Show();

            closeTextButton = FindViewById<TextView>(Resource.Id.close_edit_music_popup);
            closeTextButton.Click += CloseTextButton_Click;
        }
        
        private void CloseTextButton_Click(object sender, EventArgs e)
        {
            Dismiss();
        }
    }
}