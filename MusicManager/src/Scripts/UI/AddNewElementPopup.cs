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
    class AddNewElementPopup : Dialog
    {
        Button _submitButton;
        EditText _textField;
        TextView _headerTextView;

        string _hintText;
        string _headerText;

        Action<string> _submitCallback;

        public Action<string> SubmitCallback { set { _submitCallback = value; } }
        
        #region Constructor

        public AddNewElementPopup(string headerText, string hint, Context context) : base(context)
        {
            _hintText = hint;
            _headerText = headerText;
        }

        #endregion

        #region Lifecycle

        public override void Show()
        {
            base.Show();

            _submitButton = FindViewById<Button>(Resource.Id.popup_add_new_confirm_button);
            _textField = FindViewById<EditText>(Resource.Id.popup_add_new_element_input);
            _headerTextView = FindViewById<TextView>(Resource.Id.popup_add_new_element_header);
            
            _textField.Hint = _hintText;
            _headerTextView.Text = _headerText;

            _submitButton.Click += SubmitButton_Click;
        }

        public override void Dismiss()
        {
            _submitButton.Click -= SubmitButton_Click;

            base.Dismiss();
        }

        #endregion

        #region Events

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (_submitCallback != null)
            {
                _submitCallback(_textField.Text);
                _submitCallback = null;
            }
        }

        #endregion
    }
}