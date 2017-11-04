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
    public class Bet
    {
        public string mName;
        public double mAmount;
        public string mDetails;
        public string mOdds;
        public Outcome mOrigin;
        public double mPayout;
        public static double mWallet;

        public static List<Bet> mBets;

        public Bet()
        { }

        public Bet(string name, string details, string odds, Outcome origin, double amount, double payout)
        {
            mName = name;
            mDetails = details;
            mOdds = odds;
            mOrigin = origin;
            mAmount = amount;
        }

        public Bet(OnBetEventArgs e)
        {
            mName = e.BetOutcome.Name;
            mDetails = e.BetOutcome.Name + " : " + e.BetOutcome.oddsFor + " : " + e.BetOutcome.Prize;
            mOdds = e.BetOutcome.oddsFor;
            mOrigin = e.BetOutcome;
            mAmount = e.BetAmount;
            mPayout = e.BetOutcome.mPayout;
        }

        public void Payout()
        {
            Toast.MakeText(Application.Context, "You Won! " + mName, ToastLength.Short).Show();
            mWallet += mAmount + (mAmount * mPayout); 
        }

        public void Loss()
        {
            Toast.MakeText(Application.Context, "Sorry, you lost " + mName, ToastLength.Short).Show();
        }

        public void Roll()
        {
            Random num = new Random();
            num.Next(0, 100);
            double chance = mOrigin.probDecimal * 100;
            if (num.Next(0, 100) <= chance)
            {
                Payout();
            }
            else
            {
                Loss();
            }
        }
    }
}