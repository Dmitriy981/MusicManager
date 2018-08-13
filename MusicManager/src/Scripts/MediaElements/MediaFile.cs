using System;

namespace MusicManager
{
    class MediaFile
    {
        #region Variables

        private bool _metadataExtracted;

        #endregion


        #region Properties

        public string Url { get; set; }

        public long ID { get; set; }
        
        public MediaFileMeta Metadata { get; set; }
                
        public bool MetadataExtracted
        {
            get
            {
                return _metadataExtracted;
            }
            set
            {
                _metadataExtracted = value;
            }
        }

        #endregion


        #region Constructors



        #endregion


        #region Public methods


        #endregion
    }
}