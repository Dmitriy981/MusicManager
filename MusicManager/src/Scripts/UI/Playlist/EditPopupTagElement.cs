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
    class EditPopupTagElement
    {
        public static event Action<string> OnTagDeleteClick;

        string _tagName;

        public EditPopupTagElement(string tagName, View rootView)
        {
            _tagName = tagName;

            Button deleteButton = rootView.FindViewById<Button>(Resource.Id.edit_popup_tag_delete_button);
            deleteButton.Click += DeleteButton_Click;

            TextView tagNameView = rootView.FindViewById<TextView>(Resource.Id.edit_popup_tag_title);
            tagNameView.Text = tagName;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            OnTagDeleteClick?.Invoke(_tagName);
        }
    }
}