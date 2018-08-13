package md57e8c5f552d3d04705800ee5ca0c6848d;


public class ScreenFragment
	extends md57e8c5f552d3d04705800ee5ca0c6848d.CustomFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onViewCreated:(Landroid/view/View;Landroid/os/Bundle;)V:GetOnViewCreated_Landroid_view_View_Landroid_os_Bundle_Handler\n" +
			"n_onDestroyView:()V:GetOnDestroyViewHandler\n" +
			"";
		mono.android.Runtime.register ("MusicManager.ScreenFragment, MusicManager", ScreenFragment.class, __md_methods);
	}


	public ScreenFragment ()
	{
		super ();
		if (getClass () == ScreenFragment.class)
			mono.android.TypeManager.Activate ("MusicManager.ScreenFragment, MusicManager", "", this, new java.lang.Object[] {  });
	}


	public void onViewCreated (android.view.View p0, android.os.Bundle p1)
	{
		n_onViewCreated (p0, p1);
	}

	private native void n_onViewCreated (android.view.View p0, android.os.Bundle p1);


	public void onDestroyView ()
	{
		n_onDestroyView ();
	}

	private native void n_onDestroyView ();

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
