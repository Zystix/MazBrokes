using System.Collections.Generic;
using System;
using Android.Views;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Widget;
using Android.App;
using static MazBrokes.Resource;
using Android.Content;

namespace MazBrokes
{
    public class SlidingTabFragment : Android.Support.V4.App.Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_sample, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new SamplePagerAdapter(ChildFragmentManager);

            mSlidingTabScrollView.ViewPager = mViewPager;
        }

        public class SamplePagerAdapter : FragmentPagerAdapter
        {
            private List<Android.Support.V4.App.Fragment> mFragmentHolder;

            public SamplePagerAdapter(Android.Support.V4.App.FragmentManager fragManager) : base(fragManager)
            {
                mFragmentHolder = new List<Android.Support.V4.App.Fragment>();
                mFragmentHolder.Add(new Dashboard());
                mFragmentHolder.Add(new UpcomingEvents());
                mFragmentHolder.Add(new Fragment3());
                mFragmentHolder.Add(new Chat());
            }

            public override int Count
            {
                get { return mFragmentHolder.Count; }
            }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                return mFragmentHolder[position];
            }
        }

        public class Dashboard : Android.Support.V4.App.Fragment
        {
            private TextView wallet;
            private TextView mWelcomeText;
            
            

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {   
                
                var view = inflater.Inflate(Resource.Layout.DashBoardLayout, container, false);

                mWelcomeText = view.FindViewById<TextView>(Resource.Id.textView1);
                wallet = view.FindViewById<TextView>(Resource.Id.txtWallet);

                wallet.Text = Convert.ToString(Bet.mWallet);
                mWelcomeText.Text = "Welcome, " + MainActivity.userName + ".";
                return view;
            }

            public override string ToString() //Called on line 156 in SlidingTabScrollView
            {
                return "Dashboard";
            }
        }

        public class UpcomingEvents : Android.Support.V4.App.Fragment
        {

            public static Events currentEvent;
            public static List<Events> mEvents;
            private ListView mEventList;

            public static List<Outcome> coinOutcomes;
            public static List<Outcome> bubbleOutcomes;
            public static List<Outcome> diceOutcomes;
            public static List<Outcome> crossbarOutcomes;
            public static List<Outcome> generalOutcomes;
            
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                bubbleOutcomes = new List<Outcome>();
                coinOutcomes = new List<Outcome>();
                crossbarOutcomes = new List<Outcome>();
                diceOutcomes = new List<Outcome>();
                generalOutcomes = new List<Outcome>();

                mEvents = new List<Events>();          
                configParser.SerializeParse();

                

                var view = inflater.Inflate(Layout.UpcomingEvents, container, false);
                mEventList = view.FindViewById<ListView>(Resource.Id.listEvents);
                
                /*mEvents = new List<Events>();
                mEvents.Add(new Events("Bubble Soccer", "Team vs Team", "Its bubble soccer", bubbleOutcomes));
                mEvents.Add(new Events("Coin Toss", "Individule", "its a coin toss", coinOutcomes));
                mEvents.Add(new Events("Owen's Crossbar Challenge", "Individule", "have to hit the crossbar", crossbarOutcomes));
                mEvents.Add(new Events("Ben's Dice Roll", "Individule", "you roll dice", diceOutcomes ));
                mEvents.Add(new Events("General", "Extra", "All sorts of differnt bets that dont fit into a specific event", generalOutcomes));
                */
                
                EventRow adapter = new EventRow(Application.Context, mEvents);

                mEventList.Adapter = adapter;

                mEventList.ItemClick += MEventList_ItemClick;

                return view;
            }

            private void MEventList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
            {
                currentEvent = new Events();
                currentEvent = mEvents[e.Position];
                Intent intent = new Intent(Application.Context, typeof(EventInfoPage));
               
                /* intent.PutExtra("EventName", mEvents[e.Position].eventName);
                intent.PutExtra("EventDescription", mEvents[e.Position].Description); */
                
                
                // ^ The put Extras for the e.Position, should the 'currentEvent' apporach not work.

                this.StartActivity(intent);
                Activity.OverridePendingTransition(Animation.SlideLeft, Android.Resource.Animation.SlideOutRight);
                
                //mEvents[e.Position]

            }

            public override string ToString() //Called on line 156 in SlidingTabScrollView
            {
                return "Events";
            }
        }

        public class Fragment3 : Android.Support.V4.App.Fragment
        {
            
            private ListView mBetList;
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                var view = inflater.Inflate(Resource.Layout.Frag3Layout, container, false);
                mBetList = view.FindViewById<ListView>(Resource.Id.Frag3List);
                mBetList.Adapter = new BetListAdapter(Application.Context, Bet.mBets);
                mBetList.ItemClick += MBetList_ItemClick;
                return view;
            }

            private void MBetList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
            {
                Bet.mBets[e.Position].Roll();
                Bet.mBets.Remove(Bet.mBets[e.Position]);
            }

            public override string ToString() //Called on line 156 in SlidingTabScrollView
            {
                return "Active Bets";
            }
        }

        public class Chat : Android.Support.V4.App.Fragment
        {

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                var view = inflater.Inflate(Layout.Chat, container, false);

                return view;
            }

            public override string ToString() //Called on line 156 in SlidingTabScrollView
            {
                return "Chat";
            }

        }

    }
}
