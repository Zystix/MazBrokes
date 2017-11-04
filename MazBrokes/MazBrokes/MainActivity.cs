using Android.App;
using Android.Widget;
using Android.OS;
using System.Xml;
using System;
using Android.Views;
using Android.Support.V4.App;
using Android.Content.Res;

namespace MazBrokes
{
    [Activity(Label = "Maz Brokes", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity
    {
        public static AssetManager assets;
        private Button mBtnSignUp;
        private Button mBtnSignIn;
        private ProgressBar mprogressBar;

        public MainActivity()
        { }

        public static string userName;
        public static string userEmail;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mBtnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            mprogressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            mBtnSignUp.Click += MBtnSignUp_Click;
            mBtnSignIn.Click += MBtnSignIn_Click;
            Bet.mBets = new System.Collections.Generic.List<Bet>();
            assets = this.Assets;
        }

        private void MBtnSignIn_Click(object sender, EventArgs e)
        {
            Android.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog_SignIn signInDialog = new dialog_SignIn();
            signInDialog.Show(transaction, "dialog fragment");
            signInDialog.mOnSignInComplete += SignInDialog_mOnSignInComplete;
        }

        private void SignInDialog_mOnSignInComplete(object sender, OnSignInEventArgs e)
        {
            SetContentView(Resource.Layout.MainScreen);
            Android.Support.V4.App.FragmentManager fragmentManager = SupportFragmentManager;
            Android.Support.V4.App.FragmentTransaction transaction = fragmentManager.BeginTransaction();
            SlidingTabFragment fragment = new SlidingTabFragment();
            transaction.Replace(Resource.Id.sample_content_fragment, fragment);
            transaction.Commit();
        }

        private void MBtnSignUp_Click(object sender, System.EventArgs e)
        {
            // Pull up Dialog
            Android.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog_SignUp signUpDialog = new dialog_SignUp();
            signUpDialog.Show(transaction, "dialog fragment");

            signUpDialog.mOnSignUpComplete += SignUpDialog_mOnSignUpComplete;
        }

        private void SignUpDialog_mOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {
            // Request(e);
            userName = e.FirstName;
            userEmail = e.Email;

            SetContentView(Resource.Layout.MainScreen);
            Android.Support.V4.App.FragmentManager fragmentManager = SupportFragmentManager;
            Android.Support.V4.App.FragmentTransaction transaction = fragmentManager.BeginTransaction();
            SlidingTabFragment fragment = new SlidingTabFragment();
            transaction.Replace(Resource.Id.sample_content_fragment, fragment);
            transaction.Commit();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);

        }

        private void Request(OnSignUpEventArgs e)
        {
         
            bool userExists = false;
            XmlDocument doc = new XmlDocument();
            doc.Load("Assets\\users.xml");
            foreach (XmlNode node in doc.DocumentElement)
            {
                string email = Convert.ToString(node.Attributes[1].InnerText);
                if (email == e.Email)
                {
                    userExists = true;
                }
            }
            if (userExists == false)
            {
                doc.CreateElement("user");
            }


            RunOnUiThread(() => { mprogressBar.Visibility = Android.Views.ViewStates.Invisible;  });
        }

      /*  public Bitmap ConvertToBitmap(string fileName)
        {
            Bitmap bitmap;
            using (Stream bmpStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                bitmap = new Bitmap(image);

            }
            return bitmap;
        }
        */
    }
}

