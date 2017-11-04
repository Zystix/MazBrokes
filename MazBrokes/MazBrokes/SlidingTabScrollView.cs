using System;
using Android.Content;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MazBrokes
{
    public class SlidingTabScrollView : HorizontalScrollView
    {
        private const int TITLE_OFFSET_DIPS = 24;
        private const int TAB_VIEW_PADDING_DIPS = 16;
        private const int TAB_VIEW_TEXT_SIZE_SP = 12;

        private int mTitleOffset;

        //private int mTabViewLayoutID;
        //private int mTabViewTextViewID;

        private ViewPager mViewPager;
        private ViewPager.IOnPageChangeListener mViewPagerPageChangeListener;

        private static SlidingTabStrip mTabStrip;

        private int mScrollState;

        public interface TabColorizer
        {
            int GetIndicatorColor(int position);
            int GetDividerColor(int position);
        }

        public SlidingTabScrollView(Context context) : this(context, null)
        { }

        public SlidingTabScrollView(Context context, IAttributeSet attrs) : this(context, attrs, 0) { }

        public SlidingTabScrollView(Context context, IAttributeSet attrs, int defaultStlye) : base(context, attrs, defaultStlye)
        {
            // Disable the scroll bar
            HorizontalScrollBarEnabled = false;

            FillViewport = true;
            this.SetBackgroundColor(Android.Graphics.Color.White); // Background color of the sliding bar

            mTitleOffset = (int)(TITLE_OFFSET_DIPS * Resources.DisplayMetrics.Density);

            mTabStrip = new SlidingTabStrip(context);
            this.AddView(mTabStrip, LayoutParams.MatchParent, LayoutParams.MatchParent);
        }

        public TabColorizer CustomTabColorizer
        {
            set { mTabStrip.CustomTabColorizer = value; }
        }

        public int [] SelectedIndicatorColor
        {
            set { mTabStrip.SelectedIndicatorColors = value; }
        }

        public int [] DividerColors
        {
            set { mTabStrip.DividerColors = value; }
        }

        public ViewPager.IOnPageChangeListener OnPageListener
        {
            set { mViewPagerPageChangeListener = value; }
        }

        public ViewPager ViewPager
        {
            set
            {
                mTabStrip.RemoveAllViews();


                mViewPager = value;
                if (value != null)
                {
                    value.PageSelected += Value_PageSelected;
                    value.PageScrollStateChanged += Value_PageScrollStateChanged;
                    value.PageScrolled += Value_PageScrolled;
                    PopulatetabStrip();
                }
            }
        }

        private void Value_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {
            int tabCount = mTabStrip.ChildCount;

            if ((tabCount == 0) || (e.Position < 0) || (e.Position >= tabCount))
            {
                // if any of these conditions apply, return, no need to scroll.
                return;
            }

            mTabStrip.OnViewPagerPageChanged(e.Position, e.PositionOffset);

            View selectedTitle = mTabStrip.GetChildAt(e.Position);

            int extraOffSet = (selectedTitle != null ? (int)(e.Position * selectedTitle.Width) : 0);

            ScrollToTab(e.Position, extraOffSet);

            if (mViewPagerPageChangeListener != null)
            {
                mViewPagerPageChangeListener.OnPageScrolled(e.Position, e.PositionOffset, e.PositionOffsetPixels);
            }

        }

       

        private void Value_PageScrollStateChanged(object sender, ViewPager.PageScrollStateChangedEventArgs e)
        {
            mScrollState = e.State;
            if (mViewPagerPageChangeListener != null)
            {
                mViewPagerPageChangeListener.OnPageScrollStateChanged(e.State);
            }
        }

        private void Value_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            if (mScrollState == ViewPager.ScrollStateIdle)
            {
                mTabStrip.OnViewPagerPageChanged(e.Position, 0f);
                ScrollToTab(e.Position, 0);
            }

            if (mViewPagerPageChangeListener != null)
            {
                mViewPagerPageChangeListener.OnPageSelected(e.Position);
            }
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            if (mViewPager != null)
            {
                ScrollToTab(mViewPager.CurrentItem, 0);
            }
        }

        private void PopulatetabStrip()
        {
            PagerAdapter adapter = mViewPager.Adapter;
            for (int i = 0; i < adapter.Count; i++)
            {
                TextView tabView = CreateDefaultTabView(Context);
                tabView.Text = Convert.ToString(((SlidingTabFragment.SamplePagerAdapter)adapter).GetItem(i));
                tabView.SetTextColor(Android.Graphics.Color.Black);
                tabView.Tag = i;
                tabView.Click += TabView_Click;
                mTabStrip.AddView(tabView);
            }
        }

        private void TabView_Click(object sender, EventArgs e)
        {
            TextView clickTab = (TextView)sender;
            int pageToScrollTo = (int)clickTab.Tag;
            mViewPager.CurrentItem = pageToScrollTo;
        }

        private TextView CreateDefaultTabView(Context context)
        {
            TextView textView = new TextView(context);
            textView.Gravity = GravityFlags.Center;
            textView.SetTextSize(ComplexUnitType.Sp, TAB_VIEW_TEXT_SIZE_SP);
            textView.Typeface = Android.Graphics.Typeface.DefaultBold;

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Honeycomb)
            {
                TypedValue outValue = new TypedValue();
                Context.Theme.ResolveAttribute(Android.Resource.Attribute.SelectableItemBackground, outValue, false);
                textView.SetBackgroundResource(outValue.ResourceId);
            }

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.IceCreamSandwich)
            {
                textView.SetAllCaps(true);
            }

            int padding = (int)(TAB_VIEW_PADDING_DIPS * Resources.DisplayMetrics.Density);
            textView.SetPadding(padding, padding, padding, padding);

            return textView;
        }

        private void ScrollToTab(int tabIndex, int extraOffSet)
        {
            int tabCount = mTabStrip.ChildCount;

            if (tabCount == 0 || tabIndex < 0 || tabIndex >= tabCount)
            {
                // No need to go further, dont scroll
                return;
            }

            View selectedChild = mTabStrip.GetChildAt(tabIndex);
            if (selectedChild != null)
            {
                int scrollAmountX = selectedChild.Left + extraOffSet;

                if (tabIndex > 0 || extraOffSet > 0)
                {
                    scrollAmountX -= mTitleOffset;
                }

                this.ScrollTo(scrollAmountX, 0);
            }
        }
    }
}