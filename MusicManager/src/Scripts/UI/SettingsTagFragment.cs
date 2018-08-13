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
    public class SettingsTagFragment : CustomFragment
    {
        Button deleteButton;
        TextView tagText;
        string _name;

        #region Constructor

        public SettingsTagFragment(string name)
        {
            _name = name;
        }

        #endregion

        #region Lifecycle

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            SetView(Resource.Layout.fragment_settings_tag, inflater, container);

            deleteButton = _currentView.FindViewById<Button>(Resource.Id.settings_tag_delete);
            tagText = _currentView.FindViewById<TextView>(Resource.Id.settings_tag_name);
            tagText.Text = _name;

            deleteButton.Click += DeleteButton_Click;

            return _currentView;
        }

        public override void OnDestroyView()
        {
            deleteButton.Click -= DeleteButton_Click;

            base.OnDestroyView();
        }

        #endregion

        #region Events

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            InfoPopup popup = new InfoPopup(GetString(Resource.String.submit_delete_header),
                GetString(Resource.String.submit_delete), 
                false, true, Context);

            popup.SetButtonsDelegates(
                () =>
                {
                    RatingManager.Instance.DeleteTag(_name);
                    popup.Dismiss();
                },
                () =>
                {
                    popup.Dismiss();
                });

            popup.Show();
        }

        #endregion
    }
}