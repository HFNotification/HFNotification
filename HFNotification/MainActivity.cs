using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Java.Lang;
using System.Net;

namespace HFNotification
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Check if Internet is available
            if (isOnline())
            {
                // code here
            }
            else
            {
                // code
            }
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }

        //Check if Internet is available
        public bool isOnline()
        {
            Runtime runtime = Runtime.GetRuntime();
            try
            {
                Java.Lang.Process ipProcess = runtime.Exec("/system/bin/ping -c 1 8.8.8.8");
                int exitValue = ipProcess.WaitFor();
                return (exitValue == 0);
            }
            catch (Java.IO.IOException e) { e.PrintStackTrace(); }
            catch (InterruptedException e) { e.PrintStackTrace(); }

            return false;
        }

        // Another method to check if Internet is available
        public bool CheckInternetConnection()
        {
            string CheckUrl = "http://google.com";

            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);

                iNetRequest.Timeout = 5000;

                WebResponse iNetResponse = iNetRequest.GetResponse();

                // Console.WriteLine ("...connection established..." + iNetRequest.ToString ());
                iNetResponse.Close();

                return true;

            }
            catch (WebException ex)
            {

                // Console.WriteLine (".....no connection..." + ex.ToString ());

                return false;
            }
        }


    }
}

