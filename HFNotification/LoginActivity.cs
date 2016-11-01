
using Android.App;
using Android.OS;
using Android.Widget;
using Firebase.Iid;

namespace HFNotification
{
    [Activity(Label = "LoginActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);
            Button btnLogin= FindViewById<Button>(Resource.Id.loginbutton);
            EditText txtEmail = FindViewById<EditText>(Resource.Id.email);
            EditText txtPassword = FindViewById<EditText>(Resource.Id.password);
            btnLogin.Click += delegate {
                if(LoginService.Login(txtEmail.Text,txtPassword.Text, FirebaseInstanceId.Instance.Token))
                {
                    //TODO Go to main activity;
                }
            };
        }
    }
}