using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;

namespace MazBrokes
{
    public class OnSignUpEventArgs : EventArgs
    {
        private string mFirstName;
        private string mEmail;
        private string mPassword;

        public string FirstName
        {
            get { return mFirstName; }
            set { mFirstName = value; }
        }

        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        public OnSignUpEventArgs(string firstname, string email, string password) : base()
        {
            FirstName = firstname;
            Email = email;
            Password = password;
        }
    }

    class dialog_SignUp : DialogFragment
    {
        private EditText mTxtFirstName;
        private EditText mTxtEmail;
        private EditText mTxtPassword;
        private Button mBtnSignUp;

        

        public event EventHandler<OnSignUpEventArgs> mOnSignUpComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_sign_up, container, false);

            mTxtFirstName = view.FindViewById<EditText>(Resource.Id.txtFirstName);
            mTxtEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            mBtnSignUp = view.FindViewById<Button>(Resource.Id.btnDialogEmail);

            mBtnSignUp.Click += MBtnSignUp_Click;
            return view;
        }

        private void MBtnSignUp_Click(object sender, EventArgs e)
        {
            mOnSignUpComplete.Invoke(this, new OnSignUpEventArgs(mTxtFirstName.Text, mTxtEmail.Text, mTxtPassword.Text));
            this.Dismiss();
            
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); // sets the title bar to invisible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; // sets animations
        }

    }
}