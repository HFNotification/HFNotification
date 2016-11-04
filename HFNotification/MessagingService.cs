using Android.App;
using Android.Content;
using Android.Media;
//using Android.Support.V7.App;
using Firebase.Messaging;
using System;

namespace HFNotification
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
	public class MessagingService : FirebaseMessagingService
	{
		const string TAG = "MsgService";

		public override void OnMessageReceived(RemoteMessage firebasemessage)
		{
			// TODO: Handle FCM messages here
			StoringService.LoadMessages();
			Message message = new Message(firebasemessage.Data["NotificationUrl"], firebasemessage.Data["AlertType"], DateTime.Parse(firebasemessage.Data["CreatedDate"]));
			StoringService.Messages.Add(message);
			StoringService.SaveMessages();
			SendNotification(message);
		}
		void SendNotification(Message message)
		{
			var intent = new Intent(this, typeof(MainActivity));
			intent.AddFlags(ActivityFlags.ClearTop);
			var pendingIntent = PendingIntent.GetActivity(this, 0 /* Request code */, intent, PendingIntentFlags.OneShot);

			var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
			var notificationBuilder = new Android.Support.V4.App.NotificationCompat.Builder(this)
				.SetSmallIcon(Resource.Drawable.Icon)
				.SetContentTitle(message.AlertType)
				.SetContentText(message.CreatedDate.ToString())
				.SetAutoCancel(true)
				.SetSound(defaultSoundUri)
				.SetContentIntent(pendingIntent);

			var notificationManager = NotificationManager.FromContext(this);

			notificationManager.Notify(0 /* ID of notification */, notificationBuilder.Build());
		}
	}

}

