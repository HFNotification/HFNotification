using System;

namespace HFNotification
{
	public class Message
	{
		public Message()
		{
		}
		public Message(string url,string type, string date)
		{
			NotificationUrl = url;
			AlertType = type;
			CreatedDate = date;
			Checked = false;
		}
		public Message(string url, string type, string date,bool check)
		{
			NotificationUrl = url;
			AlertType = type;
			CreatedDate = date;
			Checked = check;
		}
		public string NotificationUrl { get; set; }
		public string AlertType { get; set; }
		public string CreatedDate { get; set; }
		public bool Checked { get; set; }
	}
}