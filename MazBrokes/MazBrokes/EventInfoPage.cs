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
    [Activity(Label = "EventInfoPage")]

    public class EventInfoPage : Activity
    {

        TextView eventTitle;
        TextView eventDescription;
        ListView outcomeList;
        public static Outcome currentOutcome;

        public EventInfoPage()
        { }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EventPage);
            eventTitle = FindViewById<TextView>(Resource.Id.txtEventName);
            eventDescription = FindViewById<TextView>(Resource.Id.txtEventDesc);
            outcomeList = FindViewById<ListView>(Resource.Id.lstBetOptions);


            eventTitle.Text = SlidingTabFragment.UpcomingEvents.currentEvent.eventName;
            eventDescription.Text = SlidingTabFragment.UpcomingEvents.currentEvent.Description;
            outcomeList.Adapter = new OutcomeListAdapter(Application.Context, SlidingTabFragment.UpcomingEvents.currentEvent.Outcomes);

            outcomeList.ItemClick += OutcomeList_ItemClick;
        }

        private void OutcomeList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            currentOutcome = new Outcome();
            currentOutcome = SlidingTabFragment.UpcomingEvents.currentEvent.Outcomes[e.Position];

            Android.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog_Outcome OutcomeDialog = new dialog_Outcome();
            OutcomeDialog.Show(transaction, "dialog fragment");
            OutcomeDialog.mOnBetComplete += OutcomeDialog_mOnBetComplete;

        }

        private void OutcomeDialog_mOnBetComplete(object sender, OnBetEventArgs e)
        {
            Bet.mBets.Add(new Bet(e));
        }
    }
}