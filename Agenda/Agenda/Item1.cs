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
    [Activity(Label = "Item1")]
    public class Item1 : Activity
    {
        Timer timer;
        int count;
        int schedTime;
        TextView text;
        MediaPlayer player;
        TimeSpan timerDisplay;
        TimeSpan convertTimer;
        TimeSpan actualTime;
        string[] items;
        string selectedItemDetails;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Item1);

            Button finish = FindViewById<Button>(Resource.Id.btnFinishItem1);
            finish.Click += Finish_Click;

            selectedItemDetails = Intent.GetStringExtra("Selected");

            bool check = NetworkInterface.GetIsNetworkAvailable();

            if (check)
            {
                Service1 client = new Service1();

                client.LoadItemDetailsAsync(selectedItemDetails);

                client.LoadItemDetailsCompleted += client_LoadItemDetailsCompleted;

            }

            else
            {
                return;
            }


        }


        private void client_LoadItemDetailsCompleted(object sender, LoadItemDetailsCompletedEventArgs e)
        {

            items = e.Result;

            var itemName = FindViewById<TextView>(Resource.Id.ItemName);
            var details = FindViewById<TextView>(Resource.Id.ItemDetails);

            itemName.Text = selectedItemDetails;
            details.Text = "Owner: " + items[0] + "\nTime Allocated: " + items[1];

            timer = new Timer();
            timerDisplay = new TimeSpan(0, 0, 0);
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
            string test = items[1];
            convertTimer = TimeSpan.Parse(test);
            schedTime = (int)convertTimer.TotalSeconds;
            count = 0;
            timer.Start();


        }



        //protected override void OnResume()
        //{
        //    base.OnResume();

        //}


        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            text = FindViewById<TextView>(Resource.Id.counter);

            count++;

            RunOnUiThread(() =>
              {
                  text.Text = "" + count;
              });

            if(count == (schedTime - 30))
            {
      
                player = MediaPlayer.Create(this, Resource.Raw.TickTock);
                player.Start();

            }

            if (count == (schedTime - 25))
            {
                player.Stop();
            }


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


        private void Finish_Click(object sender, EventArgs e)
        {

            var notes = FindViewById<EditText>(Resource.Id.editAddedNotes);

            actualTime = TimeSpan.FromSeconds(count);
            string test = actualTime.ToString();

            //AlertDialog.Builder builder = new AlertDialog.Builder(this);
            //AlertDialog alertDialog = builder.Create();
            //alertDialog.SetTitle("Time's Up!");
            //alertDialog.SetMessage("Allocated time for Item is finished!");

            //alertDialog.SetButton("Finish Item", (s, ev) =>
            //{
                base.OnBackPressed();
                timer.Stop();
                
                bool check = NetworkInterface.GetIsNetworkAvailable();

                if (check)
                {
                    Service1 client = new Service1();

                    client.UpdateItemDetailsAsync(notes.Text, test, selectedItemDetails);

                }

                else
                {
                    return;
                }



            //});


            //alertDialog.Show();


        }


    }

    }