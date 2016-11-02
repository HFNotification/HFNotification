using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace HFNotification
{
    static class StoringService
    {
        static StoringService()
        {
            Messages = new List<string>();
        }
        public static List<string> Messages { get; private set; }
        public static void SaveMessages()
        {
            var jsonMessages = JsonConvert.SerializeObject(Messages);
            using (var streamWriter = new StreamWriter(GetStorage(), true))
            {
                streamWriter.WriteLine(jsonMessages);
            }
        }
        public static void LoadMessages()
        {
            using (var streamReader = new StreamReader(GetStorage()))
            {
                string jsonMessages = streamReader.ReadToEnd();
                Messages = JsonConvert.DeserializeObject<List<string>>(jsonMessages);
            }

        }
        private static string GetStorage()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "messages.dat");
        }
    }
}