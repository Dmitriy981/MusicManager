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
    class PlaylistItem
    {
        public MediaFile Music { get; set; }
        public PlaylistItemFragment Fragment { get; set; }


        public PlaylistItem(MediaFile music, PlaylistItemFragment fragment)
        {
            Music = music;
            Fragment = fragment;
        }
    }
}