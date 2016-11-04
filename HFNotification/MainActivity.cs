using System.Net.Mail;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Preferences;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace HFNotification
{
	[Activity(Label = "@string/ApplicationName", Icon = "@drawable/icon")]
	public class MainActivity : AppCompatActivity
	{
		private TextView tvUserName;

		const string TAG = "MainActivity";
		DrawerLayout drawerLayout;
		protected override void OnCreate(Bundle savedInstanceState)
		{

			base.OnCreate(savedInstanceState);
			StoringService.LoadMessages();
			SetContentView(Resource.Layout.Main);
			drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			// Init toolbar
			var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
			SetSupportActionBar(toolbar);
			SupportActionBar.SetTitle(Resource.String.ApplicationName);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetDisplayShowHomeEnabled(true);
			Android.Util.Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
			// Attach item selected handler to navigation view
			var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

			
			var headerView = navigationView.GetHeaderView(0);
			tvUserName = headerView.FindViewById<TextView>(Resource.Id.userName);
			ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
			string email = preferences.GetString("email", "User@gmail.com");

			//var email = "User@gmail.com";
			string name = email.Split('@')[0];
			tvUserName.Text = name;

			// Create ActionBarDrawerToggle button and add it to the toolbar
			var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
			drawerLayout.SetDrawerListener(drawerToggle);
			drawerToggle.SyncState();

			//load default home screen
			var ft = FragmentManager.BeginTransaction();
			ft.AddToBackStack(null);
			ft.Add(Resource.Id.HomeFrameLayout, new HomeFragment());
			ft.Commit();
		}
		//define custom title text
		protected override void OnResume()
		{
			StoringService.LoadMessages();
			SupportActionBar.SetTitle(Resource.String.ApplicationName);
			base.OnResume();
		}
		//define action for navigation menu selection
		void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{
			PackageManager manager = this.PackageManager;
			PackageInfo info = manager.GetPackageInfo(this.PackageName, 0);
			string version = info.VersionName;

			switch (e.MenuItem.ItemId)
			{
				case (Resource.Id.nav_messages):
					Toast.MakeText(this, "Messages selected!", ToastLength.Short).Show();
					break;
				case (Resource.Id.nav_about):
					Toast.MakeText(this, version, ToastLength.Long).Show();
					break;
				case (Resource.Id.nav_logout):
					Toast.MakeText(this, "You was successfully logout!", ToastLength.Long).Show();
					LoginService.Logout();
					StartActivity(typeof(LoginActivity));
					Finish();
					break;
				case (Resource.Id.nav_exit):
					//Toast.MakeText(this, "Exit selected!", ToastLength.Short).Show();
					//System.Environment.Exit(0);
					Finish();
					break;
			}
			// Close drawer
			drawerLayout.CloseDrawers();
		}

		//add custom icon to tolbar
		//public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
		//{
		//	MenuInflater.Inflate(Resource.Menu.action_menu, menu);
		//	if (menu != null)
		//	{
		//		menu.FindItem(Resource.Id.action_refresh).SetVisible(true);
		//		menu.FindItem(Resource.Id.action_attach).SetVisible(false);
		//	}
		//	return base.OnCreateOptionsMenu(menu);
		//}
		//define action for tolbar icon press
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Android.Resource.Id.Home:
					//this.Activity.Finish();
					return true;
				//case Resource.Id.action_attach:
					//FnAttachImage();
					//return true;
				default:
					return base.OnOptionsItemSelected(item);
			}
		}
		//to avoid direct app exit on backpreesed and to show fragment from stack
		public override void OnBackPressed()
		{
			if (FragmentManager.BackStackEntryCount != 1)
			{
				FragmentManager.PopBackStack();// fragmentManager.popBackStack();
			}
			else
			{
					Finish();
				//	base.OnBackPressed();
			}
		}
	}
}