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
using Android.Media;
using System.IO;
using Android.Provider;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using Android.Database;
using Java.IO;
using Newtonsoft.Json;

namespace MusicManager
{
    class MusicManager : LoadSingleton<MusicManager>
    {
        Dictionary<string, MediaFile> allMusics;

        public Dictionary<string, MediaFile> AllMusics { get { return allMusics ?? (allMusics = new Dictionary<string, MediaFile>()); } }

        #region lifecycle

        public void Initialize()
        {
            SetAllMusics();
        }
        
        public void Save()
        {
            SaveManager.Save(this);
        }

        #endregion

        #region Music

        private void SetAllMusics()
        {
            string[] projection = {
                    MediaStore.Audio.Media.InterfaceConsts.Id,
                    MediaStore.Audio.Media.InterfaceConsts.AlbumId,
                    MediaStore.Audio.Media.InterfaceConsts.Title,
                    MediaStore.Audio.Media.InterfaceConsts.Artist,
                    MediaStore.Audio.Media.InterfaceConsts.Data,
                    MediaStore.Audio.Media.InterfaceConsts.Album,
                    MediaStore.Audio.Media.InterfaceConsts.DisplayName,
                    MediaStore.Audio.Media.InterfaceConsts.Duration,
                    MediaStore.Audio.Media.InterfaceConsts.Year
                };

            var loader = new CursorLoader(GUIManager.Instance.CurrentActivity.ApplicationContext, MediaStore.Audio.Media.ExternalContentUri, projection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();
            string placeholderArtPath = "android.resource://" + GUIManager.Instance.CurrentActivity.ApplicationContext.PackageName + "/" + Resource.Drawable.album_cover_placeholder;
            if (cursor.MoveToFirst())
            {
                do
                {
                    long currentID = cursor.GetLong(cursor.GetColumnIndex(projection[0]));
                    string displayTitle = GetStringFromCursor(cursor, projection[6], "Unknown title");

                    if (!AllMusics.ContainsKey(displayTitle))
                    {
                        MediaFile file = new MediaFile();
                        file.Metadata = new MediaFileMeta();

                        long albumId = cursor.GetLong(cursor.GetColumnIndex(projection[1]));

                        file.ID = currentID;
                        file.Metadata.Title = GetStringFromCursor(cursor, projection[2], "Unknown title");
                        file.Metadata.Artist = GetStringFromCursor(cursor, projection[3], "Unknown Artist");
                        file.Url = cursor.GetString(cursor.GetColumnIndex(projection[4]));
                        file.Metadata.Album = GetStringFromCursor(cursor, projection[5], "Unknown Album");
                        file.Metadata.DisplayTitle = DeleteFileMask(displayTitle);
                        file.Metadata.Duration = cursor.GetInt(cursor.GetColumnIndex(projection[7]));
                        file.Metadata.AlbumID = albumId;
                        file.Metadata.AlbumArtUri = GetAlbumArtFromCursor(cursor, albumId, placeholderArtPath);

                        AllMusics.Add(displayTitle, file);
                    }
                } while (cursor.MoveToNext());
            }
        }
        
        public void PlayPlaylist(string[] categories = null, string[] tags = null)
        {
            List<string> resultPlaylist = new List<string>();

            foreach (KeyValuePair<string, MediaFile> musicPair in AllMusics)
            {
                resultPlaylist.Add(musicPair.Key);
            }

            PlayerManager.Instance.PlayPlaylist(resultPlaylist);
        }

        #endregion


        #region Helper methods

        public MediaFile MusicByName(string name)
        {
            if (AllMusics.ContainsKey(name))
            {
                return AllMusics[name];
            }

            return AllMusics.First().Value;
        }

        public static List<string> AvailableCategoriesForMusic(MediaFile media)
        {
            List<string> result = new List<string>();

            foreach (string category in RatingManager.Instance.Categories)
            {
                if (!media.Categories.ContainsKey(category))
                {
                    result.Add(category);
                }
            }

            return result;
        }

        public static List<string> AvailableTagsForMusic(MediaFile media)
        {
            List<string> result = new List<string>();

            foreach (string category in RatingManager.Instance.Tags)
            {
                if (!media.Categories.ContainsKey(category))
                {
                    result.Add(category);
                }
            }

            return result;
        }

        private string GetStringFromCursor(ICursor cursor, string key, string unknownString)
        {
            string result = cursor.GetString(cursor.GetColumnIndex(key)).Trim();
            result = result.Equals("<unknown>") ? unknownString : result;
            return result;
        }

        private string DeleteFileMask(string name)
        {
            string[] splitted = name.Split('.');
            string result = splitted[0];
            for (int i = 1; i < splitted.Length - 1; i++)
            {
                result += "." + splitted[i];
            }

            return result;
        }

        private string GetAlbumArtFromCursor(ICursor cursor, long albumId, string placeholder)
        {
            var songCover = Android.Net.Uri.Parse("content://media/external/audio/albumart");
            if (albumId != 5)
            {
                return ContentUris.WithAppendedId(songCover, albumId).ToString();
            }

            return placeholder;
        }

        #endregion
    }
}