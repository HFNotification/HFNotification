using System;
using Android.App;
using Firebase.Iid;
using System.Net;

namespace HFNotification
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class IIDService : FirebaseInstanceIdService
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
            //TODO: (!Dont send token from here in future ) add login check.
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://requestb.in/14q7hw11");
            httpWebRequest.Headers.Add("Token:" + token);
            httpWebRequest.Method = "POST";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        }
    }
}
