using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Agenda
{
    class HomeAdapter : BaseAdapter<TableItem>
    {
        List<TableItem> items;
        Activity context;

        public HomeAdapter(Activity context, List<TableItem> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override TableItem this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;

            if (view == null)
            view = context.LayoutInflater.Inflate(Resource.Layout.HomeCustomRow, null);
            view.FindViewById<TextView>(Resource.Id.MeetingNameText).Text = item.MeetingName;
            view.FindViewById<TextView>(Resource.Id.MeetingTimeText).Text = item.MeetingTime;
            view.FindViewById<TextView>(Resource.Id.MeetingLocationText).Text = item.MeetingLocation;
            return view;

        }


    }
}