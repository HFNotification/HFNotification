using Android.App;
using Android.Gms.Common;
using Android.Net;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Net;
using System.Threading.Tasks;


namespace HFNotification
{
	[Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
	public class StartUpActivity : Activity
	{
		//const string TAG = "MainActivity";
		private TextView msgText;
		private Button tryButton;
		private ProgressBar startProgressBar;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
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
			// Set our view from the "main" layout resource
			//		SetContentView(Resource.Layout.StartUp);
			//		msgText = FindViewById<TextView>(Resource.Id.msgText);
			//		tryButton = FindViewById<Button>(Resource.Id.tryAgainButton);
			//		startProgressBar = FindViewById<ProgressBar>(Resource.Id.startProgressBar);
			//		tryButton.Text = GetString(Resource.String.tryButtonText);
			//		//Check if Internet is available 

			//		TryConnection();
			//		startProgressBar.Visibility = ViewStates.Gone;
			//		tryButton.Visibility = ViewStates.Visible;
			//		tryButton.Click += async delegate {
			//			tryButton.Visibility = ViewStates.Gone;
			//			startProgressBar.Visibility = ViewStates.Visible;
			//			await Task.Factory.StartNew(() => {

			//				TryConnection();
			//				RunOnUiThread(() => startProgressBar.Visibility = ViewStates.Gone);
			//				RunOnUiThread(() => tryButton.Visibility = ViewStates.Visible);
			//			});
			//		};
			//	}


			//	public void TryConnection()
			//	{
			//		for (int tryconnection = 1; tryconnection <= Constants.TRYCOUNT; tryconnection++)
			//		{
			//			if (IsOnline())
			//			{
			//				if (IsPlayServicesAvailable())
			//				{
			//					try
			//					{
			//						if (!LoginService.Login())
			//						{
			//							StartActivity(typeof(LoginActivity));
			//							Finish();
			//						}
			//						else
			//						{
			//							StartActivity(typeof(MainActivity));
			//							Finish();
			//						}
			//					}
			//					catch (WebException)
			//					{
			//						RunOnUiThread(() => msgText.Text = "Loginization failed!");
			//					}
			//				}
			//			}
			//			else
			//			{
			//				RunOnUiThread(() => msgText.Text = "No internet connection!");
			//			}
			//			System.Threading.Thread.Sleep(500);
			//		}
			//	}

			//	//Check if Internet is available
			//	public bool IsOnline()
			//	{
			//		ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
			//		NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
			//		return (activeConnection != null) && activeConnection.IsConnected;
			//	}
			//	public bool IsPlayServicesAvailable()
			//	{
			//		int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			//		if (resultCode != ConnectionResult.Success)
			//		{
			//			if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
			//				msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
			//			else
			//			{
			//				RunOnUiThread(() => msgText.Text = "Sorry, this device is not supported :(");
			//				Finish();
			//			}
			//			return false;
			//		}
			//		else
			//		{
			//			RunOnUiThread(() => msgText.Text = "GooglePlay services currently available");
			//			return true;
			//		}
		}
	}
}