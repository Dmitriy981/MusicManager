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
    class InfoPopup : Dialog
    {
        string _headerText;
        string _text;
        string _leftButtonText;
        string _rightButtonText;
        bool _isOneButton;

        bool _isWasShow = false;

        Button _leftButton;
        Button _leftButtonOne;
        Button _rigthButton;
        TextView _infoTextView;
        TextView _headerTextView;

        LinearLayout _oneButtonRoot;
        LinearLayout _twoButtonsRoot;

        Action _leftButtonCallback = null;
        Action _rigthButtonCallback = null;

        #region Constructor

        public InfoPopup(string header, string text, 
            bool isOneButton, bool isDefaultLayout,
            Context context) : base(context)
        {
            _headerText = header;
            _text = text;
            _isOneButton = isOneButton;

            _leftButtonText = GUIManager.Instance.CurrentActivity.GetString(Resource.String.ok);
            _rightButtonText = GUIManager.Instance.CurrentActivity.GetString(Resource.String.cancel);

            if (isDefaultLayout)
            {
                SetContentView(Resource.Layout.popup_info);
            }
        }

        #endregion

        #region Lifecycle

        public override void Dismiss()
        {
            _leftButton.Click -= LeftButton_Click;
            _rigthButton.Click -= RigthButton_Click;
            _leftButtonOne.Click -= LeftButton_Click;

            _leftButtonCallback = null;
            _rigthButtonCallback = null;

            base.Dismiss();
        }

        public override void Show()
        {
            base.Show();

            _headerTextView = FindViewById<TextView>(Resource.Id.info_popup_header);
            _infoTextView = FindViewById<TextView>(Resource.Id.info_popup_info);
            _leftButton = FindViewById<Button>(Resource.Id.info_popup_2_left_button);
            _rigthButton = FindViewById<Button>(Resource.Id.info_popup_2_right_button);
            _leftButtonOne = FindViewById<Button>(Resource.Id.info_popup_1_button);
            _twoButtonsRoot = FindViewById<LinearLayout>(Resource.Id.info_popup_two_buttons_root);
            _oneButtonRoot = FindViewById<LinearLayout>(Resource.Id.info_popup_one_button_root);

            _leftButton.Click += LeftButton_Click;
            _rigthButton.Click += RigthButton_Click;
            _leftButtonOne.Click += LeftButton_Click;

            _twoButtonsRoot.Visibility = _isOneButton ? ViewStates.Gone : ViewStates.Visible;
            _oneButtonRoot.Visibility = _isOneButton ? ViewStates.Visible : ViewStates.Gone;

            _isWasShow = true;
            SetButtonsTexts(_leftButtonText, _rightButtonText);
            SetHeaderText(_headerText);
            SetInfoText(_text);
        }

        #endregion


        #region Builder

        public void SetButtonsTexts(string leftText, string rightText = "")
        {
            _leftButtonText = leftText;
            _rightButtonText = rightText;

            SetTextOnViewAfterShow(_leftButton, _leftButtonText);
            SetTextOnViewAfterShow(_leftButtonOne, _leftButtonText);
            SetTextOnViewAfterShow(_rigthButton, _rightButtonText);
        }

        public void SetHeaderText(string text)
        {
            _headerText = text;

            SetTextOnViewAfterShow(_headerTextView, _headerText);
        }

        public void SetInfoText(string text)
        {
            _text = text;

            SetTextOnViewAfterShow(_infoTextView, text);
        }

        private void SetTextOnViewAfterShow(TextView view, string text)
        {
            if (_isWasShow)
            {
                view.Text = text;
            }
        }

        public void SetLeftButtonDelegate(Action callback)
        {
            _leftButtonCallback = callback;
        }

        public void SetRigthButtonDelegate(Action callback)
        {
            _rigthButtonCallback = callback;
        }

        public void SetButtonsDelegates(Action leftCallback = null, Action rigthCallback = null)
        {
            SetLeftButtonDelegate(leftCallback);
            SetRigthButtonDelegate(rigthCallback);
        }

        #endregion

        #region Events

        private void RigthButton_Click(object sender, EventArgs e)
        {
            if (_rigthButtonCallback != null)
            {
                _rigthButtonCallback();
                _rigthButtonCallback = null;
            }
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            if (_leftButtonCallback != null)
            {
                _leftButtonCallback();
                _leftButtonCallback = null;
            }
        }

        #endregion
    }
}