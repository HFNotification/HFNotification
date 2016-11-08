using Android.Views;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;

namespace HFNotification
{
	public class MessageListAdapter : BaseAdapter<Message>
	{
		public List<Message> Items { get; set; }
		public Context Context { get; set; }
		public MessageListAdapter(Context context, List<Message> items) : base()
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
			TextView messageview = view.FindViewById<TextView>(Resource.Id.textView1);
			messageview.Text = string.Format(Items[position].AlertType+"\n"+Items[position].CreatedDate);
			CheckBox messageviewcheck = view.FindViewById<CheckBox>(Resource.Id.checkBox1);
			messageviewcheck.Checked= Items[position].Checked;
			return view;
		}
		public void Update(List<Message> newitems)
		{
			Items.Clear();
			Items.AddRange(newitems);
			NotifyDataSetChanged();
		}
	}
}