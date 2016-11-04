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
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.homeLayout, container, false);
			myListView = view.FindViewById<ListView>(Resource.Id.lvNotifications);
			//string[] items = new string[] { "https://www.xamarin.com/", "https://github.com/", "https://firebase.google.com/docs/cloud-messaging/", "https://tortoisegit.org/", "Bulbs", "Tubers", "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers", "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers3" };
			//string[] items =StoringService.Messages.ToArray();
			myListView.Adapter = new ArrayAdapter<Message>(view.Context, Android.Resource.Layout.SimpleDropDownItem1Line, StoringService.Messages);
			myListView.ItemClick += (sender, e) =>
			{
				var url = myListView.GetItemAtPosition(e.Position).ToString();
				var uri = Android.Net.Uri.Parse(url);
				var intent = new Intent(Intent.ActionView, uri);
				myListView.GetChildAt(e.Position).SetBackgroundColor(Android.Graphics.Color.White);
				StartActivity(intent);
			};
			
			return view;
		}
	}
}