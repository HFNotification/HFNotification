using System;

namespace HFNotification
{
	public class Message
	{
		public Message()
		{
		}
		public Message(string url,string type, DateTime date)
		{
			NotificationUrl = url;
			AlertType = type;
			CreatedDate = date;
			Checked = false;
		}
		public Message(string url, string type, DateTime date,bool check)
		{
			NotificationUrl = url;
			AlertType = type;
			CreatedDate = date;
			Checked = check;
		}
		public string NotificationUrl { get; set; }
		public string AlertType { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool Checked { get; set; }
	}
}