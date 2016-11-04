using System;
using Android.Views;
using Android.Widget;
using Android.Content;
using System.Collections.ObjectModel;

namespace HFNotification
{
	public class MessageListAdapter : BaseAdapter<Message>
	{
		public ObservableCollection<Message> Items { get; set; }
		public Context Context { get; set; }
		public MessageListAdapter(Context context, ObservableCollection<Message> items) : base()
		{
			Items = items;
			Context = context;
		}
		public override Message this[int position]
		{
			get
			{
				return Items[position];
			}
		}

		public override int Count
		{
			get
			{
				return Items.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView;
			if (view == null)
			{
				view = LayoutInflater.From(Context).Inflate(Resource.Layout.rowlayout,null);
			}
			CheckedTextView messageview = view.FindViewById<CheckedTextView>(Resource.Id.txtMessage);
			messageview.Text = string.Format(Items[position].AlertType+"\n"+Items[position].CreatedDate);
			messageview.Checked = Items[position].Checked;
			return view;
		}
	}
}