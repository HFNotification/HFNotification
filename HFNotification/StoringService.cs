using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace HFNotification
{
	public static class StoringService
	{
		static StoringService()
		{
			Messages = new List<Message>();
		}
		public static List<Message> Messages {get;private set; }
		public static void SaveMessages()
		{
			var jsonMessages = JsonConvert.SerializeObject(Messages);
			using (var streamWriter = new StreamWriter(GetStorage()))
			{
				streamWriter.WriteLine(jsonMessages);
			}
		}
		public static void LoadMessages()
		{
			if (File.Exists(GetStorage()))
			{
				using (var streamReader = new StreamReader(GetStorage()))
				{
					string jsonMessages = streamReader.ReadToEnd();
					Messages = JsonConvert.DeserializeObject<List< Message>> (jsonMessages);
				}
			}
			

		}
		private static string GetStorage()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "messages.dat");
		}
	}
}