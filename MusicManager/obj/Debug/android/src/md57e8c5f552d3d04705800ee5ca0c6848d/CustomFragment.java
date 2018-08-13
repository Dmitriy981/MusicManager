package md57e8c5f552d3d04705800ee5ca0c6848d;


public class CustomFragment
	extends android.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MusicManager.CustomFragment, MusicManager", CustomFragment.class, __md_methods);
	}


	public CustomFragment ()
	{
		super ();
		if (getClass () == CustomFragment.class)
			mono.android.TypeManager.Activate ("MusicManager.CustomFragment, MusicManager", "", this, new java.lang.Object[] {  });
	}

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
