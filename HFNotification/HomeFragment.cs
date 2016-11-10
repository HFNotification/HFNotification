using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;

namespace HFNotification
{
	public class HomeFragment : Fragment
	{
		private ListView listView;
		private MessageListAdapter adapter;
		private SwipeRefreshLayout refresher;
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			StoringService.LoadMessages();
			View view = inflater.Inflate(Resource.Layout.HomeLayout, container, false);
			listView = view.FindViewById<ListView>(Resource.Id.lvNotifications);
			adapter = new MessageListAdapter(view.Context, StoringService.Messages);
			listView.Adapter = adapter;
			refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
			refresher.Refresh += (sender, e) => {
				adapter.Update(StoringService.Messages);
				refresher.Refreshing = false;
			};
			listView.ItemClick += (sender, e) =>
			{
				try
				{
					string url = StoringService.Messages[StoringService.Messages.Count - e.Position - 1].NotificationUrl;
					StoringService.Messages[StoringService.Messages.Count - e.Position - 1].Checked = true;
					StoringService.LoadMessages();
					adapter.Update(StoringService.Messages);
					var uri = Android.Net.Uri.Parse(url);
					Intent intent = new Intent(Intent.ActionView, uri);
					StartActivity(intent);
				}
				catch
				{
					AlertDialog.Builder alert = new AlertDialog.Builder(view.Context);
					alert.SetTitle("Error :");
					alert.SetMessage("This message is not a url.");
					alert.SetPositiveButton("Close", (senderAlert, args) =>
					{
						Toast.MakeText(view.Context, "Closed!", ToastLength.Short).Show();
					});
					Dialog dialog = alert.Create();
					dialog.Show();
				}
			};
			return view;
		}
		public override void OnResume()
		{
			base.OnResume();
			StoringService.LoadMessages();
		}
		public override void OnPause()
		{
			base.OnPause();
			StoringService.SaveMessages();
		}
	}
}