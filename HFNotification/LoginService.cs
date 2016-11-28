using System.Net;
using System.IO;
using Android.Content;
using Android.Preferences;
using Android.App;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HFNotification
{
	static public class LoginService
	{
		static public string Token
		{
			get
			{
				ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
				return (preferences.GetString("token", "notoken"));
			}
		}
		static public string UserName
		{
			get
			{
				ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
				return (preferences.GetString("login", "nologin"));
			}
		}
		static public bool LoginStatus
		{
			get
			{
				ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
				return (preferences.GetBoolean("loged", false));
			}
		}
		static public bool Login(string email, string password, string token)
		{
			if (NewCredentials(email, password, token))
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
		static public async Task<bool> LoginAsync(string email, string password, string token)
		{
			string data = string.Format("email={0}&password={1}&token={2}", email, password, token);
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constants.NEWCREDSURL);
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.Method = "POST";
			//WebUtility.UrlEncode(data);
			httpWebRequest.ContentLength = data.Length;
			using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
			{
				streamWriter.Write(data);
				streamWriter.Flush();
			}
			var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
			string resultstring;
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				resultstring = await streamReader.ReadToEndAsync();
				RequestResult requestResult = JsonConvert.DeserializeObject<RequestResult>(resultstring);
				if (requestResult.Result)
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

		}
		static public bool Login()
		{
			//Login using local stored credentials
			ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
			if (preferences.GetBoolean("loged", false))
			{
				if (NewCredentials(preferences.GetString("email", "nomail"), preferences.GetString("password", "nopassword"), preferences.GetString("token", "notoken")))
				{
					return true;
				}
			}
			return false;
		}

		static private bool NewCredentials(string email, string password, string token)
		{
			string data = string.Format("email={0}&password={1}&token={2}", email, password, token);
			return SendRequest(data, Constants.NEWCREDSURL);
		}
		static public bool UpdateCredentials(string email, string password, string newtoken, string oldtoken)
		{
			string data = string.Format("email={0}&password={1}&newtoken={2}&oldtoken={3}", email, password, newtoken, oldtoken);
			return SendRequest(data, Constants.UPDATECREDSURL);
		}
		static private bool SendRequest(string data, string url)
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
			RequestResult result = JsonConvert.DeserializeObject<RequestResult>(resultstring);
			return result.Result;

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