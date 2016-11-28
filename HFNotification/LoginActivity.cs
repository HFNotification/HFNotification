using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using System;

namespace HFNotification
{
	[Activity(Label = "LoginActivity", Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
	public class LoginActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Login);
			Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
			EditText etxtEmail = FindViewById<EditText>(Resource.Id.etxtEmail);
			EditText etxtPassword = FindViewById<EditText>(Resource.Id.etxtPassword);
			TextView txtError = FindViewById<TextView>(Resource.Id.txtError);
			ProgressBar loginProgressBar = FindViewById<ProgressBar>(Resource.Id.loginProgressBar);
			loginProgressBar.Visibility = ViewStates.Invisible;
			btnLogin.Click += async delegate {
				try
				{
					loginProgressBar.Visibility = ViewStates.Visible;
					btnLogin.Enabled = false;
					bool val = await LoginService.LoginAsync(etxtEmail.Text, etxtPassword.Text, FirebaseInstanceId.Instance.Token);
					if (val)
					{
						StartActivity(typeof(MainActivity));
						Finish();
					}
					else
					{
						txtError.Text = GetString(Resource.String.wrong_password);
						loginProgressBar.Visibility = ViewStates.Invisible;
						btnLogin.Enabled = true;
					}
				}
				catch (Exception)
				{
					txtError.Text = GetString(Resource.String.wrong_url);
					loginProgressBar.Visibility = ViewStates.Invisible;
					btnLogin.Enabled = true;
				}
			};
		}
	}
}