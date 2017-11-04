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

namespace MazBrokes
{
    public class OnBetEventArgs : EventArgs
    {
        private double mBetAmount;
        private Outcome mBetOutcome;
        

        public double BetAmount
        {
            get { return mBetAmount; }
            set { mBetAmount = value; }
        }

        public Outcome BetOutcome
        {
            get { return mBetOutcome; }
            set { mBetOutcome = value; }
        }

        public OnBetEventArgs(double betamount, Outcome outcome) : base()
        {
            BetAmount = betamount;
            BetOutcome = outcome;
        }
    }

    class dialog_Outcome : DialogFragment
    {
        private EditText mBetAmount;
        private Button mBtnPlaceBet;
        private TextView OutcomeDetails;

        public event EventHandler<OnBetEventArgs> mOnBetComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_Bet, container, false);

            mBetAmount = view.FindViewById<EditText>(Resource.Id.txtBetAmount);
            mBtnPlaceBet = view.FindViewById<Button>(Resource.Id.btnPlaceBet);
            OutcomeDetails = view.FindViewById<TextView>(Resource.Id.txtOutcomeDetails);

            OutcomeDetails.Text = EventInfoPage.currentOutcome.Name + " : " + EventInfoPage.currentOutcome.oddsFor + " : " + EventInfoPage.currentOutcome.Prize;

            mBtnPlaceBet.Click += MBtnPlaceBet_Click;
            return view;

        }

        private void MBtnPlaceBet_Click(object sender, EventArgs e)
        {
            if (mBetAmount.Text == null)
            {

            }
            else {
            mOnBetComplete.Invoke(this, new OnBetEventArgs(Convert.ToDouble(mBetAmount.Text), EventInfoPage.currentOutcome));
            this.Dismiss();
            }

        }



        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); // sets the title bar to invisible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; // sets animations
        }

    }
}