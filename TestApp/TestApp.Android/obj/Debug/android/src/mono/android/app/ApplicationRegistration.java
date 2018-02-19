package mono.android.app;

public class ApplicationRegistration {

	public static void registerApplications ()
	{
				// Application and Instrumentation ACWs must be registered first.
		mono.android.Runtime.register ("TestApp.Droid.MainApplication, TestApp.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", md52fbe3486fee5a5395f40f60a143816fe.MainApplication.class, md52fbe3486fee5a5395f40f60a143816fe.MainApplication.__md_methods);
		
	}
}
