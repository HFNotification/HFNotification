
using Android.App;
using Android.OS;
using Android.Widget;
using Firebase.Iid;
using System.Net;

namespace HFNotification
{
	[Activity(Label = "LoginActivity", Theme = "@android:style/Theme.Material.Light")]
	public class LoginActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Login);
			Button btnLogin= FindViewById<Button>(Resource.Id.loginbutton);
			EditText txtEmail = FindViewById<EditText>(Resource.Id.email);
			EditText txtPassword = FindViewById<EditText>(Resource.Id.password);
			TextView txtError = FindViewById<TextView>(Resource.Id.txterror);
			btnLogin.Click += delegate {
				try
				{
					if (LoginService.Login(txtEmail.Text, txtPassword.Text, FirebaseInstanceId.Instance.Token))
					{
						StartActivity(typeof(MainActivity));
						Finish();
					}
					else
					{
						txtError.Text = "Login or password is incorrect";
					}
				}
				catch (WebException)
				{
					txtError.Text = "Could not resolve connection to server";
				}
			};
		}
	}
}