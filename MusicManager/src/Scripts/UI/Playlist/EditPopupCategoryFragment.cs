using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MusicManager
{
    class EditPopupCategoryElement
    {
        public static event Action<string, int> OnCategoryChanged;

        private const string CIRCLE_ID_PREFIX = "edit_popup_category_circle_";
        private const int IMAGES_COUNT = 5;
        private const int FILL_CIRCLE_SRC = Resource.Drawable.black_circle;
        private const int EMPTY_CIRCLE_SRC = Resource.Drawable.white_circle;

        private List<ImageView> _images;
        string _categoryName;

        public EditPopupCategoryElement(int rating, string categoryName, View rootView)
        {
            _categoryName = categoryName;
            _images = new List<ImageView>();

            TextView categoryText = rootView.FindViewById<TextView>(Resource.Id.edit_popup_category_title);
            categoryText.Text = categoryName;

            for (int i = 0; i < IMAGES_COUNT; i++)
            {
                ImageView image = rootView.FindViewById<ImageView>(HelperClass.GetIdByString(CIRCLE_ID_PREFIX + i));
                _images.Add(image);
                SetClickListener(i, image);
            }

            FillImages(rating);
        }

        private void SetClickListener(int i, ImageView image)
        {
            image.Click += (sender, e) =>
            {
                FillImages(i + 1);
                OnCategoryChanged?.Invoke(_categoryName, i + 1);
            };
        }

        private void FillImages(int currentRating)
        {
            for (int i = 0; i < _images.Count; i++)
            {
                _images[i].SetImageResource(i < currentRating ? FILL_CIRCLE_SRC : EMPTY_CIRCLE_SRC);
            }
        }
    }
}