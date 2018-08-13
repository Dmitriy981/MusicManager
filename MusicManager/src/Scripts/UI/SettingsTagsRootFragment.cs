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
    public class SettingsTagsRootFragment : Fragment
    {
        View currentView;
        LinearLayout tagsRoot;
        Button addTagButton;

        Dictionary<string, SettingsTagFragment> tags;

        public Dictionary<string, SettingsTagFragment> Tags { get { return tags ?? (tags = new Dictionary<string, SettingsTagFragment>()); } }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            currentView = inflater.Inflate(Resource.Layout.fragment_settings_tags_root, container, false);

            tagsRoot = currentView.FindViewById<LinearLayout>(Resource.Id.settings_tags_root);
            addTagButton = currentView.FindViewById<Button>(Resource.Id.settings_add_tag);

            addTagButton.Click += AddTagButton_Click; ;
            RatingManager.OnTagsUpdated += RatingManager_OnTagsUpdated;

            FillTags();

            return currentView;
        }

        public override void OnDestroyView()
        {
            addTagButton.Click -= AddTagButton_Click;
            RatingManager.OnTagsUpdated -= RatingManager_OnTagsUpdated;

            base.OnDestroyView();
        }

        #region Working Process

        private void FillTags()
        {
            FragmentTransaction tx = FragmentManager.BeginTransaction();
            foreach (String name in RatingManager.Instance.Tags)
            {
                AddTag(name, tx);
            }

            tx.AddToBackStack(null);
            tx.Commit();
        }

        private void AddTag(String name, FragmentTransaction tx = null)
        {
            bool isCommit = tx == null;
            tx = isCommit ? FragmentManager.BeginTransaction() : tx;

            SettingsTagFragment fragment = new SettingsTagFragment(name);
            tx.Add(Resource.Id.settings_tags_root, fragment);
            Tags.Add(name, fragment);

            if (isCommit)
            {
                tx.AddToBackStack(null);
                tx.Commit();
            }
        }

        private void DeleteTag(String name)
        {
            FragmentTransaction tx = FragmentManager.BeginTransaction();
            tx.Remove(Tags[name]);
            tx.AddToBackStack(null);
            tx.Commit();
        }

        #endregion


        #region Events

        private void RatingManager_OnTagsUpdated(string name, bool isAdded)
        {
            if (isAdded)
            {
                AddTag(name);
            }
            else
            {
                DeleteTag(name);
            }
        }

        private void AddTagButton_Click(object sender, EventArgs e)
        {
            AddNewElementPopup popup = new AddNewElementPopup(GetString(Resource.String.add_tag),
                GetString(Resource.String.add_tag_hint), Context);

            popup.SubmitCallback = (newTag) =>
            {
                RatingManager.Instance.AddTag(newTag);
                popup.Dismiss();
            };

            popup.SetContentView(Resource.Layout.popup_add_new_element);
            popup.Show();
        }

        #endregion
    }
}