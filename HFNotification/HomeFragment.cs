using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace HFNotification
{
	public class HomeFragment : Fragment
	{
		private ListView myListView;
		MessageListAdapter adapter;
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			StoringService.LoadMessages();
			View view = inflater.Inflate(Resource.Layout.homeLayout, container, false);
			myListView = view.FindViewById<ListView>(Resource.Id.lvNotifications);
			//string[] items = new string[] { "https://www.xamarin.com/", "https://github.com/", "https://firebase.google.com/docs/cloud-messaging/", "https://tortoisegit.org/", "Bulbs", "Tubers", "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers", "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers3" };
			//string[] items =StoringService.Messages.ToArray();
			//myListView.Adapter = new ArrayAdapter<Message>(view.Context, Android.Resource.Layout.SimpleDropDownItem1Line, StoringService.Messages);
			adapter = new MessageListAdapter(view.Context, StoringService.Messages);
			myListView.Adapter = adapter;
			myListView.ItemClick += (sender, e) =>
			{
				try
				{
					var url = StoringService.Messages[e.Position].NotificationUrl;
					StoringService.Messages[e.Position].Checked = true;
					adapter.Update(StoringService.Messages);
					var uri = Android.Net.Uri.Parse(url);
					var intent = new Intent(Intent.ActionView, uri);
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
			//StoringService.LoadMessages();
		}
		public override void OnPause()
		{
			base.OnPause();
			StoringService.SaveMessages();
		}
	}
}