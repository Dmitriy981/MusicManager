package md57e8c5f552d3d04705800ee5ca0c6848d;


public class PlaylistItemFragment
	extends android.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"n_onDestroyView:()V:GetOnDestroyViewHandler\n" +
			"";
		mono.android.Runtime.register ("MusicManager.PlaylistItemFragment, MusicManager", PlaylistItemFragment.class, __md_methods);
	}


	public PlaylistItemFragment ()
	{
		super ();
		if (getClass () == PlaylistItemFragment.class)
			mono.android.TypeManager.Activate ("MusicManager.PlaylistItemFragment, MusicManager", "", this, new java.lang.Object[] {  });
	}

	public PlaylistItemFragment (md57e8c5f552d3d04705800ee5ca0c6848d.PlaylistItemFragment p0)
	{
		super ();
		if (getClass () == PlaylistItemFragment.class)
			mono.android.TypeManager.Activate ("MusicManager.PlaylistItemFragment, MusicManager", "MusicManager.PlaylistItemFragment, MusicManager", this, new java.lang.Object[] { p0 });
	}


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);


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
