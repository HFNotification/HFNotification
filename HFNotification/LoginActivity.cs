using Android.App;
using Android.OS;
using Android.Widget;
using Firebase.Iid;
using System.Net;

namespace HFNotification
{
	[Activity(Label = "LoginActivity",Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
	public class LoginActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Login);
			Button btnLogin= FindViewById<Button>(Resource.Id.btnLogin);
			EditText etxtEmail = FindViewById<EditText>(Resource.Id.etxtEmail);
			EditText etxtPassword = FindViewById<EditText>(Resource.Id.etxtPassword);
			TextView txtError = FindViewById<TextView>(Resource.Id.txtError);
			btnLogin.Click += delegate {
				try
				{
					if (LoginService.Login(etxtEmail.Text, etxtPassword.Text, FirebaseInstanceId.Instance.Token))
					{
						StartActivity(typeof(MainActivity));
						Finish();
					}
					else
					{
						txtError.Text = GetString(Resource.String.wrong_password);
					}
				}
				catch (WebException)
				{
					txtError.Text = GetString(Resource.String.wrong_url);
				}
			};
		}
	}
}