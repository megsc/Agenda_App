using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using System.Net.NetworkInformation;
using Agenda.au.edu.wa.central.mydesign.student;


namespace Agenda
{
    [Activity(Label = "Agenda1")]
    public class Agenda1 : Activity
    {
        Timer timer;
        int count;
        int schedTime;
        MediaPlayer player;
        TimeSpan timerDisplay;
        TimeSpan convertTimer;
        TimeSpan actualTime;
        string[] items;
        string selectedItem;

        List<TableItem> tableItems = new List<TableItem>();
        ListView listview;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Agenda1);

            listview = FindViewById<ListView>(Resource.Id.AgendaList);

            Button home = FindViewById<Button>(Resource.Id.home);
            home.Click += Home_Click;

            Button start = FindViewById<Button>(Resource.Id.btnStartMeeting);
            start.Click += Start_Click;

            selectedItem = Intent.GetStringExtra("Selected");

            bool check = NetworkInterface.GetIsNetworkAvailable();

            if (check)
            {
                Service1 client = new Service1();

                client.LoadItemsAsync(selectedItem);

                client.LoadItemsCompleted += client_LoadItemsCompleted;

            }

            else
            {
                return;
            }



            if (check)
            {
                Service1 client = new Service1();

                client.LoadMeetingDetailsAsync(selectedItem);

                client.LoadMeetingDetailsCompleted += client_LoadMeetingDetailsCompleted;

            }

            else
            {
                return;
            }



            listview.Adapter = new AgendaAdapter(this, tableItems);
            listview.ItemClick += Listview_ItemClick;

            Button end = FindViewById<Button>(Resource.Id.btnEndMeeting);
            end.Click += End_Click;

        }

        private void client_LoadItemsCompleted(object sender, LoadItemsCompletedEventArgs e)
        {

            string[] items = e.Result;

            for (int i = 0; i < items.Length; i++)
            {
                tableItems.Add(new TableItem() { ItemName = items[i] });
            }

            listview.Adapter = new AgendaAdapter(this, tableItems);
            listview.ItemClick += Listview_ItemClick;


        }


        private void client_LoadMeetingDetailsCompleted(object sender, LoadMeetingDetailsCompletedEventArgs e)
        {
                items = e.Result;

                var meetingName = FindViewById<TextView>(Resource.Id.meetingName);
                var details = FindViewById<TextView>(Resource.Id.meetingDetails);

                meetingName.Text = selectedItem;
                details.Text = "Meeting Date: " + items[1] + "\nMeeting Time: " + items[2] + "\nMeeting Location: " + items[0] + "\nAttendees: " + items[3] + "\nMinutes: " + items[4] + "\nAbsentees: " + items[5];


        }



        private void Listview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listview = sender as ListView;
            var t = tableItems[e.Position];
            string selected = t.ItemName;
            var intent = new Intent(this, typeof(Item1));
            intent.PutExtra("Selected", selected);
            StartActivity(intent);
    
        }


        private void End_Click(object sender, EventArgs e)
        {
            //AlertDialog.Builder builder = new AlertDialog.Builder(this);
            //AlertDialog alertDialog = builder.Create();
            //alertDialog.SetTitle("Time's Up!");
            //alertDialog.SetMessage("Allocated time for Meeting is finished!");

            actualTime = TimeSpan.FromSeconds(count);
            string test = actualTime.ToString();

            //alertDialog.SetButton("Finish Meeting", (s, ev) =>
            //{
                
                base.OnBackPressed();
                timer.Stop();

                bool check = NetworkInterface.GetIsNetworkAvailable();

                if (check)
                {
                    Service1 client = new Service1();

                    client.UpdateMeetingTimeAsync(test, selectedItem);

                }

                else
                {
                    return;
                }


            //});

            //alertDialog.SetButton2("Extend Meeting", (s, ev) =>
            //{


            //});

            //alertDialog.Show();


        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            count++;

            if (count == schedTime)
            {
                player = MediaPlayer.Create(this, Resource.Raw.Ship_Brass_Bell);
                player.Start();
            }

            if (count == (schedTime + 5))
            {
                player.Stop();
            }




        }

        private void Start_Click(object sender, EventArgs e)
        {
            timer = new Timer();
            timerDisplay = new TimeSpan(0, 0, 0);
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
            string test = items[2];
            convertTimer = TimeSpan.Parse(test);
            schedTime = (int)convertTimer.TotalSeconds;
            count = 0;
            timer.Start();


        }

        private void Home_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Home));
            StartActivity(intent);
        }




    }
}