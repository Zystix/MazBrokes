package md581806179e5b6c21c79d23ff487cd6f38;


public class SlidingTabFragment_SamplePagerAdapter
	extends android.support.v4.app.FragmentPagerAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getCount:()I:GetGetCountHandler\n" +
			"n_getItem:(I)Landroid/support/v4/app/Fragment;:GetGetItem_IHandler\n" +
			"";
		mono.android.Runtime.register ("MazBrokes.SlidingTabFragment+SamplePagerAdapter, MazBrokes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SlidingTabFragment_SamplePagerAdapter.class, __md_methods);
	}


	public SlidingTabFragment_SamplePagerAdapter (android.support.v4.app.FragmentManager p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == SlidingTabFragment_SamplePagerAdapter.class)
			mono.android.TypeManager.Activate ("MazBrokes.SlidingTabFragment+SamplePagerAdapter, MazBrokes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Support.V4.App.FragmentManager, Xamarin.Android.Support.v4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();


	public android.support.v4.app.Fragment getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native android.support.v4.app.Fragment n_getItem (int p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
