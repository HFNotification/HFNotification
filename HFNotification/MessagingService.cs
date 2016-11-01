using Android.App;
using Android.Content;
using Android.Media;
//using Android.Support.V7.App;
using Firebase.Messaging;

namespace HFNotification
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
	public class MessagingService : FirebaseMessagingService
	{
		const string TAG = "MsgService";

		public override void OnMessageReceived(RemoteMessage message)
		{
			// TODO: Handle FCM messages here.
			SendNotification(message.Data["msg"]);
		}

		/**
         * Create and show a simple notification containing the received FCM message.
         */
		void SendNotification(string messageBody)
		{
			var intent = new Intent(this, typeof(MainActivity));
			intent.AddFlags(ActivityFlags.ClearTop);
			var pendingIntent = PendingIntent.GetActivity(this, 0 /* Request code */, intent, PendingIntentFlags.OneShot);

			var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
			var notificationBuilder = new Android.Support.V4.App.NotificationCompat.Builder(this)
				.SetSmallIcon(Resource.Drawable.Icon)
				.SetContentTitle("Hey ho")
				.SetContentText(messageBody)
				.SetAutoCancel(true)
				.SetSound(defaultSoundUri)
				.SetContentIntent(pendingIntent);

			var notificationManager = NotificationManager.FromContext(this);

			notificationManager.Notify(0 /* ID of notification */, notificationBuilder.Build());
		}
	}

}

