using Android.App;
using Firebase.Iid;
using System.Net;
using Newtonsoft.Json;
using System.IO;

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
			var refreshedToken = FirebaseInstanceId.Instance.Token;
			Android.Util.Log.Debug(TAG, "Refreshed token: " + refreshedToken);
			SendRegistrationToServer(FirebaseInstanceId.Instance.Token);
		}

        void SendRegistrationToServer(string token)
        {
            //TODO: Refresh token on server side.
        }
    }
}
