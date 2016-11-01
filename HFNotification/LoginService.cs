using System.Net;
using Newtonsoft.Json;
using System.IO;
using Android.Content;
using Android.Preferences;
using Android.App;

namespace HFNotification
{
	static public class LoginService
	{
		static public bool Login(string email, string password, string token)
		{
			//Login using user inputed credentials
			if (SendCredentials(email,password,token))
			{
				ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
				ISharedPreferencesEditor editor = preferences.Edit().Clear();
				editor.PutString("email", email);
				editor.PutString("password", password);
				editor.PutString("token", token);
				editor.PutBoolean("loged", true);
				editor.Apply();
				return true;
			}
			return false;
		}
		static public bool Login()
		{
			//Login using local stored credentials
			ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
			if (preferences.GetBoolean("loged",false))
			{
				if(SendCredentials(preferences.GetString("email", "nomail"), preferences.GetString("password", "nopassword"), preferences.GetString("token", "notoken")))
				{
					return true;
				}
			}
			return false;
		}
	   static private bool SendCredentials(string email, string password, string token)
		{
			var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://requestb.in/14q7hw11");
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";
			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				string json = JsonConvert.SerializeObject(new
				{
					email = email,
					password = password,
					deviceid = token
				});
				streamWriter.Write(json);
				streamWriter.Flush();
			}
			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			string result;
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				result = streamReader.ReadToEnd();
			}
			//TODO: parse resulting json
			return true;
		}
		//TODO Logout method
	}
}