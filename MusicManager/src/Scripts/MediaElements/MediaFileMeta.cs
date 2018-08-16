using System;
using System.ComponentModel;


namespace MusicManager
{
    class MediaFileMeta
    {
        public string Album { get; set; }

        public string AlbumArtUri { get; set; }

#warning delete
        public long AlbumID { get; set; }

        public string Artist { get; set; }

        public string DisplayTitle { get; set; }

        public int Duration { get; set; }

        public string Title { get; set; }
    }
}