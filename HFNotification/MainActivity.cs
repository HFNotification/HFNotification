using Android.App;
using Android.Gms.Common;
using Android.Net;
using Android.OS;
using Android.Widget;


namespace HFNotification
{
	[Activity(Label = "@string/ApplicationName", Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		const string TAG = "MainActivity";
		TextView msgText;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
		}
	}
}