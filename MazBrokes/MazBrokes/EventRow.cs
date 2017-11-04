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
    class EventRow : BaseAdapter<Events>
    {
        private List<Events> Rows;
        private Context mContext;

        public EventRow(Context context, List<Events> items)
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

        public override Events this[int positon]
        {
            get { return Rows[positon]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.EventRow, null, false);
            }

            TextView txtEvent = row.FindViewById<TextView>(Resource.Id.txtEvent);
            txtEvent.Text = Rows[position].eventName + "\n\n" + Rows[position].eventType;
            //txtEvent.SetCompoundDrawablesWithIntrinsicBounds(Rows[position].image, null, null, null);

            return row;
        }


    }
}