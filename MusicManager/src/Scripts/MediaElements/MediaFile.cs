using System;
using System.Collections.Generic;

namespace MusicManager
{
    class MediaFile
    {
        #region Variables

        List<string> _tags;
        Dictionary<string, int> _categories;

        #endregion


        #region Properties

        public string Url { get; set; }

        public long ID { get; set; }

        public MediaFileMeta Metadata { get; set; }

        public bool IsOld { get; set; }

        public List<string> Tags
        {
            get { return _tags ?? (_tags = new List<string>()); }
            set { _tags = value; }
        }

        public Dictionary<string, int> Categories
        {
            get { return _categories ?? (_categories = new Dictionary<string, int>()); }
            set { _categories = value; }
        }

        #endregion


        #region Constructors



        #endregion


        #region Public methods

        public void AddCategory(string name)
        {
            if (!_categories.ContainsKey(name))
            {
                _categories.Add(name, 0);
            }
        }

        public void ChangeCategory(string name, int value)
        {
            if (_categories.ContainsKey(name))
            {
                _categories[name] = value;
            }
        }
        
        public void AddTag(string name)
        {
            if (!_tags.Contains(name))
            {
                _tags.Add(name);
            }
        }

        #endregion
    }
}