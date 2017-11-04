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
    class OnBetFinishEventArgs : EventArgs
    {
        private Bet mBetOutcome;

        public Bet BetOutcome
        {
            get { return mBetOutcome; }
            set { mBetOutcome = value; }
        }

        public OnBetFinishEventArgs(Bet outcome) : base()
        {
            BetOutcome = outcome;
        }

    }
    class BetListAdapter : BaseAdapter<Bet>
    {
        private List<Bet> Rows;
        private Context mContext;
        private Bet currentBet;

        private void setCurrent(Bet bet)
        {
            currentBet = bet;
        }

        public BetListAdapter(Context context, List<Bet> items)
        {
            Rows = items;
            mContext = context;
        }


        public override int Count
        {
            get { return Rows.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Bet this[int positon]
        {
            get { return Rows[positon]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            Bet currentBet = Rows[position];
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.BetListRow, null, false);
            }

            TextView txtTitle = row.FindViewById<TextView>(Resource.Id.txtBetListTitle);
            txtTitle.Text = Rows[position].mDetails;

            
            TextView txtAmount = row.FindViewById<TextView>(Resource.Id.txtBetListAmount);
            txtAmount.Text = Convert.ToString(Rows[position].mAmount);

            TextView txtOdds = row.FindViewById<TextView>(Resource.Id.txtBetListOdds);
            txtOdds.Text = Rows[position].mOdds;         

            return row;
        }
    }
}