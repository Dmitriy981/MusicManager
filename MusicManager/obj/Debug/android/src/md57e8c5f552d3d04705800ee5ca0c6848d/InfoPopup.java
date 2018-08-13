package md57e8c5f552d3d04705800ee5ca0c6848d;


public class InfoPopup
	extends android.app.Dialog
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_dismiss:()V:GetDismissHandler\n" +
			"n_show:()V:GetShowHandler\n" +
			"";
		mono.android.Runtime.register ("MusicManager.InfoPopup, MusicManager", InfoPopup.class, __md_methods);
	}


	public InfoPopup (android.content.Context p0)
	{
		super (p0);
		if (getClass () == InfoPopup.class)
			mono.android.TypeManager.Activate ("MusicManager.InfoPopup, MusicManager", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public InfoPopup (android.content.Context p0, boolean p1, android.content.DialogInterface.OnCancelListener p2)
	{
		super (p0, p1, p2);
		if (getClass () == InfoPopup.class)
			mono.android.TypeManager.Activate ("MusicManager.InfoPopup, MusicManager", "Android.Content.Context, Mono.Android:System.Boolean, mscorlib:Android.Content.IDialogInterfaceOnCancelListener, Mono.Android", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public InfoPopup (android.content.Context p0, int p1)
	{
		super (p0, p1);
		if (getClass () == InfoPopup.class)
			mono.android.TypeManager.Activate ("MusicManager.InfoPopup, MusicManager", "Android.Content.Context, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}


	public void dismiss ()
	{
		n_dismiss ();
	}

	private native void n_dismiss ();


	public void show ()
	{
		n_show ();
	}

	private native void n_show ();

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
