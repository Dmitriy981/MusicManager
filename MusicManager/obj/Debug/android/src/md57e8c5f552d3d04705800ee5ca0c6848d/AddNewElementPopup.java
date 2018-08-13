package md57e8c5f552d3d04705800ee5ca0c6848d;


public class AddNewElementPopup
	extends android.app.Dialog
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_show:()V:GetShowHandler\n" +
			"n_dismiss:()V:GetDismissHandler\n" +
			"";
		mono.android.Runtime.register ("MusicManager.AddNewElementPopup, MusicManager", AddNewElementPopup.class, __md_methods);
	}


	public AddNewElementPopup (android.content.Context p0)
	{
		super (p0);
		if (getClass () == AddNewElementPopup.class)
			mono.android.TypeManager.Activate ("MusicManager.AddNewElementPopup, MusicManager", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public AddNewElementPopup (android.content.Context p0, boolean p1, android.content.DialogInterface.OnCancelListener p2)
	{
		super (p0, p1, p2);
		if (getClass () == AddNewElementPopup.class)
			mono.android.TypeManager.Activate ("MusicManager.AddNewElementPopup, MusicManager", "Android.Content.Context, Mono.Android:System.Boolean, mscorlib:Android.Content.IDialogInterfaceOnCancelListener, Mono.Android", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public AddNewElementPopup (android.content.Context p0, int p1)
	{
		super (p0, p1);
		if (getClass () == AddNewElementPopup.class)
			mono.android.TypeManager.Activate ("MusicManager.AddNewElementPopup, MusicManager", "Android.Content.Context, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}


	public void show ()
	{
		n_show ();
	}

	private native void n_show ();


	public void dismiss ()
	{
		n_dismiss ();
	}

	private native void n_dismiss ();

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
