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
    public class OnSignInEventArgs : EventArgs
    {
        private string mEmail;
        private string mPassword;

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

        public OnSignInEventArgs(string email, string password) : base()
        {
            Email = email;
            Password = password;
        }
    }

    class dialog_SignIn : DialogFragment
    {
        private EditText mTxtEmail;
        private EditText mTxtPassword;
        private Button mBtnSignIn;

        public event EventHandler<OnSignInEventArgs> mOnSignInComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_sign_in, container, false);

            mTxtEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            mBtnSignIn = view.FindViewById<Button>(Resource.Id.btnDialogSignIn);

            mBtnSignIn.Click += MBtnSignIn_Click;
            return view;
        }

        private void MBtnSignIn_Click(object sender, EventArgs e)
        {
            mOnSignInComplete.Invoke(this, new OnSignInEventArgs(mTxtEmail.Text, mTxtPassword.Text));
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