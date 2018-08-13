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
    class RatingManager : LoadSingleton<RatingManager>
    {
        public static event Action<string, bool> OnTagsUpdated; //name, isAdded
        public static event Action<string, bool> OnCategoriesUpdated; //name, isAdded

        public const string PROPERTY_TAGS = "Tags";
        public const string PROPERTY_CATEGORIES = "Categories";
        string[] propertiesForJSON;

        List<string> _tags;
        List<string> _categoties;

        string[] PropertiesForJSON { get { return propertiesForJSON ?? (propertiesForJSON = new string[] { PROPERTY_TAGS, PROPERTY_CATEGORIES }); } }
        public List<string> Tags {
            get { return _tags ?? (_tags = new List<string>()); }
            set { _tags = value; }
        }
        public List<string> Categories {
            get { return _categoties ?? (_categoties = new List<string>()); }
            set { _categoties = value; }
        }

        #region lifecycle

        public void Initialize()
        {

        }


        public void Save()
        {
            SaveManager.Save(this);
        }

        #endregion

        #region Categories

        public void AddCategory(string newCategory)
        {
            if (Categories.Contains(newCategory)) return;

            Categories.Add(newCategory);
            UpdateCategories(newCategory, true);
        }

        public void DeleteCategory(string category)
        {
            if (!Categories.Contains(category)) return;

            Categories.Remove(category);
            UpdateCategories(category, false);
        }

        void UpdateCategories(string key, bool isAdded)
        {
            Save();
            OnCategoriesUpdated?.Invoke(key, isAdded);
        }

        #endregion

        #region Tags

        public void AddTag(string newTag)
        {
            if (Tags.Contains(newTag)) return;

            Tags.Add(newTag);
            UpdateTags(newTag, true);
        }

        public void DeleteTag(string tag)
        {
            if (!Tags.Contains(tag)) return;

            Tags.Remove(tag);
            UpdateTags(tag, false);
        }

        void UpdateTags(string key, bool isAdded)
        {
            Save();
            OnTagsUpdated?.Invoke(key, isAdded);
        }

        #endregion
    }
}