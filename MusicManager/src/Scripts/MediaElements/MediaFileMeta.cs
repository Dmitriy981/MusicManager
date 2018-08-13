using System;
using System.ComponentModel;


namespace MusicManager
{
    class MediaFileMeta
    {
        public string Album { get; set; }

        public string AlbumArtist { get; set; }

        public string AlbumArtUri { get; set; }

        public long AlbumID { get; set; }

        public string Artist { get; set; }

        public DateTime Date { get; set; }

        public string DisplayTitle { get; set; }

        public int Duration { get; set; }

        public string Genre { get; set; }

        public string Title { get; set; }

        public long Year { get; set; }
    }
}