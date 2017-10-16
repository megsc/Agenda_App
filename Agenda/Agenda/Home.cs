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
using System.Net.NetworkInformation;
using Agenda.au.edu.wa.central.mydesign.student;

namespace Agenda
{
    [Activity(Label = "Home")]
    public class Home : Activity
    {
        List<TableItem> tableItems = new List<TableItem>();
        ListView listview;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Home);

            listview = FindViewById<ListView>(Resource.Id.HomeList);

            Button change = FindViewById<Button>(Resource.Id.change);
            change.Click += Change_Click;


            bool check = NetworkInterface.GetIsNetworkAvailable();

            if (check)
            {
                Service1 client = new Service1();

                client.LoadMeetingsAsync();
                
                client.LoadMeetingsCompleted += client_LoadMeetingsCompleted;

            }

            else
            {
                return;
            }



        }

        private void client_LoadMeetingsCompleted(object sender, LoadMeetingsCompletedEventArgs e)
        {

            string[] meetings = e.Result;

            for (int i = 0; i < meetings.Length; i++)
            { 
                tableItems.Add(new TableItem() { MeetingName = meetings[i] });
            }

            listview.Adapter = new HomeAdapter(this, tableItems);
            listview.ItemClick += Listview_ItemClick;


        }



        private void Listview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listview = sender as ListView;
            var t = tableItems[e.Position];
            string selected = t.MeetingName;
            var intent = new Intent(this, typeof(Agenda1));
            intent.PutExtra("Selected", selected);
            StartActivity(intent);
        }



        private void Change_Click(object sender, EventArgs e)
        {
            var list = FindViewById<RelativeLayout>(Resource.Id.HomeListView);
            
            
                list.SetBackgroundColor(Android.Graphics.Color.Coral);

            

        }



    }
}