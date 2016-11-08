using System.Net;
using System.IO;
using Android.Content;
using Android.Preferences;
using Android.App;
using Newtonsoft.Json;

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
				ISharedPreferencesEditor editor = preferences.Edit();
				editor.PutString("email", email)
					.PutString("password", password)
					.PutString("token", token)
					.PutBoolean("loged", true)
					.Apply();
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
			string data = string.Format("email={0}&password={1}&token={2}",email, password, token);
			return SendRequest(data, "http://requestb.in/12dmtqp1");
		}
		static public bool UpdateCredentials(string email, string password, string newtoken, string oldtoken)
		{
			string data = string.Format("email={0}&password={1}&newtoken={2}&oldtoken={3}", email, password, newtoken, oldtoken);
			return SendRequest(data, "http://requestb.in/12dmtqp1");
		}
		static private bool SendRequest(string data,string url)
		{
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
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
			string resultstring;
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				resultstring = streamReader.ReadToEnd();
			}
			//RequestResult result = JsonConvert.DeserializeObject<RequestResult>(resultstring);
			//return result.Result;
			return true;
			
		}
		static public bool Logout()
		{
			ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
			ISharedPreferencesEditor editor = preferences.Edit();
			editor.PutBoolean("loged", false);
			editor.Apply();
			return true;
		}
	}
}