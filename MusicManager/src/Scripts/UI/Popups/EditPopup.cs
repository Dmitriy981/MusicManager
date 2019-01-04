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

        Spinner _categoriesSpinner;
        Button _addCategoryButton;
        LinearLayout _categoriesRoot;

        Spinner _tagsSpinner;
        Button _addTagButton;
        LinearLayout _tagsRoot;

        List<EditPopupCategoryElement> _categories;
        List<EditPopupTagElement> _tags;

        public EditPopup(Context context) : base(context)
        {
        }

        public void Initialize(MediaFile music)
        {
            editMusic = music;
        }

        private void SetTags()
        {
            if (editMusic.Tags.Count == 0)
                return;

            if (_tags != null)
                _tags.Clear();

            _tagsRoot.RemoveAllViews();
            _tags = new List<EditPopupTagElement>();
            DebugManager.Log("Set tags00");
            foreach (string tag in editMusic.Tags)
            {
                View tagView = LayoutInflater.Inflate(Resource.Layout.edit_popup_tag_fragment, null);
                _tags.Add(new EditPopupTagElement(tag, tagView));
                _tagsRoot.AddView(tagView);
            }
        }

        private void SetCategories()
        {
            if (editMusic.Categories.Count == 0)
                return;

            if (_categories != null)
                _categories.Clear();
            
            _categoriesRoot.RemoveAllViews();
            _categories = new List<EditPopupCategoryElement>();
            foreach (KeyValuePair<string, int> category in editMusic.Categories)
            {
                View categoryView = LayoutInflater.Inflate(Resource.Layout.edit_popup_category_fragment, null);
                _categories.Add(new EditPopupCategoryElement(category.Value, category.Key, categoryView));
                _categoriesRoot.AddView(categoryView);
            }
        }

        public override void Dismiss()
        {
            closeTextButton.Click -= CloseTextButton_Click;
            _addTagButton.Click -= AddTagButton_Click;
            _addCategoryButton.Click -= AddCategoryButtonClick;
            
            EditPopupTagElement.OnTagDeleteClick -= EditPopupTagElement_OnTagDeleteClick;
            EditPopupCategoryElement.OnCategoryChanged -= EditPopupCategoryElement_OnCategoryChanged;

            base.Dismiss();
        }

        public override void Show()
        {
            base.Show();

            CreatePopup();
        }

        private void CreatePopup()
        {
            Activity cueerentActivity = GUIManager.Instance.CurrentActivity;
            closeTextButton = FindViewById<TextView>(Resource.Id.close_edit_music_popup);
            closeTextButton.Click += CloseTextButton_Click;

            _categoriesSpinner = FindViewById<Spinner>(Resource.Id.categories_spinner);
            _categoriesSpinner.Adapter = GUIManager.ArrayAdapterForList(
                MusicManager.AvailableCategoriesForMusic(editMusic),
                HelperClass.GetStringByKey(Resource.String.edit_popup_category_hint));

            _tagsSpinner = FindViewById<Spinner>(Resource.Id.tags_spinner);
            _tagsSpinner.Adapter = GUIManager.ArrayAdapterForList(
                MusicManager.AvailableTagsForMusic(editMusic),
                HelperClass.GetStringByKey(Resource.String.edit_popup_tag_hint));

            _addTagButton = FindViewById<Button>(Resource.Id.music_add_tag);
            _addTagButton.Click += AddTagButton_Click;
            _addCategoryButton = FindViewById<Button>(Resource.Id.music_add_category);
            _addCategoryButton.Click += AddCategoryButtonClick;

            EditPopupTagElement.OnTagDeleteClick += EditPopupTagElement_OnTagDeleteClick;
            EditPopupCategoryElement.OnCategoryChanged += EditPopupCategoryElement_OnCategoryChanged;

            _categoriesRoot = FindViewById<LinearLayout>(Resource.Id.edit_music_fragment_categories_root);
            _tagsRoot = FindViewById<LinearLayout>(Resource.Id.edit_music_fragment_tags_root);
            titleText = FindViewById<TextView>(Resource.Id.edit_popup_title);
            titleText.Text = editMusic.Metadata.DisplayTitle;

            SetCategories();
            SetTags();
        }

        #region Helper methods

        #endregion

                private void EditPopupCategoryElement_OnCategoryChanged(string categoryName, int newRating)
        {
            editMusic.ChangeCategory(categoryName, newRating);
        }

        private void EditPopupTagElement_OnTagDeleteClick(string tagName)
        {
            editMusic.DeleteTag(tagName);
            SetTags();
        }

        private void CloseTextButton_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        private void AddTagButton_Click(object sender, EventArgs e)
        {
            if (_tagsSpinner.SelectedItemPosition > 0)
            {
                editMusic.AddTag(_tagsSpinner.SelectedItem.ToString());
                SetTags();
            }
        }

        private void AddCategoryButtonClick(object sender, EventArgs e)
        {
            if (_categoriesSpinner.SelectedItemPosition > 0)
            {
                editMusic.AddCategory(_categoriesSpinner.SelectedItem.ToString());
                SetCategories();
            }
        }
    }
}