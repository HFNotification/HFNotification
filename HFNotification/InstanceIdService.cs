using Android.App;
using Firebase.Iid;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Android.Preferences;
using Android.Content;

namespace HFNotification
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
	public class InstanceIdentificationService : FirebaseInstanceIdService
	{
		const string TAG = "IIDService";

		/**
		* Called if InstanceID token is updated. This may occur if the security of
		* the previous token had been compromised
		*/
		public override void OnTokenRefresh()
		{
			// Get updated InstanceID token.
			// TODO: Implement this method to send any registration to your app's servers.
			string refreshedToken = FirebaseInstanceId.Instance.Token;
			Android.Util.Log.Debug(TAG, "Refreshed token: " + refreshedToken);
			SendRegistrationToServer(FirebaseInstanceId.Instance.Token);
		}

		void SendRegistrationToServer(string token)
		{
			ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
			if(preferences.GetString("token","notoken")!="notoken")
			{
				LoginService.UpdateCredentials(preferences.GetString("email", "nomail"), preferences.GetString("password", "nopassword"), FirebaseInstanceId.Instance.Token, preferences.GetString("token", "notoken"));
			}
		}
	}
}
