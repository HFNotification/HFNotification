using Android.App;
using Android.Gms.Common;
using Android.Net;
using Android.OS;
using Android.Widget;
using System.Net;


namespace HFNotification
{
	[Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
	public class StartUpActivity : Activity
	{
		//const string TAG = "MainActivity";
		TextView msgText;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.StartUp);
			//Check if Internet is available
			if (isOnline())
			{
				//Test if GPS is available
				msgText = FindViewById<TextView>(Resource.Id.msgText);
				if (IsPlayServicesAvailable())
				{
					try
					{
						if (!LoginService.Login())
						{
							StartActivity(typeof(LoginActivity));
							Finish();
						}
						else
						{
							StartActivity(typeof(MainActivity));
							Finish();
						}
					}
					catch (WebException)
					{
						//TODO show message Couldnt not connect ot server
					}
				}
			}
			else
			{
				// code
				//MessageBox.Show("Internet connections are not available");
				Toast.MakeText(this, "Internet connections are not available", ToastLength.Long).Show();
			}
		}
		//Check if Internet is available
		public bool isOnline()
		{
			ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
			NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
			return (activeConnection != null) && activeConnection.IsConnected;
		}
		public bool IsPlayServicesAvailable()
		{
			int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			if (resultCode != ConnectionResult.Success)
			{
				if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
					msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
				else
				{
					msgText.Text = "Sorry, this device is not supported";
					Finish();
				}
				return false;
			}
			else
			{
				msgText.Text = "Google Play Services is available.";
				return true;
			}
		}
	}
}