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
    class OutcomeListAdapter : BaseAdapter<Outcome>
    {
        private List<Outcome> Rows;
        private Context mContext;

        public OutcomeListAdapter(Context context, List<Outcome> items)
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

        public override Outcome this[int positon]
        {
            get { return Rows[positon]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.OutcomeListRow, null, false);
            }

            TextView txtEvent = row.FindViewById<TextView>(Resource.Id.OutcomeRow);
            txtEvent.Text = Rows[position].Name + " : " + Rows[position].oddsFor + " : " + Rows[position].Prize;
            

            return row;
        }
    }
}