using System.Net;
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
			if (NewCredentials(email,password,token))
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
				if(NewCredentials(preferences.GetString("email", "nomail"), preferences.GetString("password", "nopassword"), preferences.GetString("token", "notoken")))
				{
					return true;
				}
			}
			return false;
		}

		static private bool NewCredentials(string email, string password, string token)
		{
			var data = "email=" + email;
			data += "&password=" + password;
			data += "&token=" + token;
			return SendRequest(data);
		}
		static public bool UpdateCredentials(string email, string password, string newtoken, string oldtoken)
		{
			var data = "email=" + email;
			data += "&password=" + password;
			data += "&newtoken=" + newtoken;
			data += "&oldtoken=" + oldtoken;
			return SendRequest(data);
		}
		static private bool SendRequest(string data)
		{
			var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://requestb.in/pk05y2pk");
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.Method = "POST";
			//WebUtility.UrlEncode(data);
			httpWebRequest.ContentLength = data.Length;
			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				streamWriter.Write(data);
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