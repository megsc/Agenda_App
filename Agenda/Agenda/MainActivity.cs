using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.NetworkInformation;
using Agenda.au.edu.wa.central.mydesign.student;

namespace Agenda
{
    [Activity(Label = "Agenda", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.btnLogin);

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            //var editUsername = this.FindViewById<EditText>(Resource.Id.editUsername);
            //var editPassword = this.FindViewById<EditText>(Resource.Id.editPassword);

            bool check = NetworkInterface.GetIsNetworkAvailable();

            if (check)
            {
                var editUsername = FindViewById<EditText>(Resource.Id.editUsername);
                var editPassword = FindViewById<EditText>(Resource.Id.editPassword);

                Service1 client = new Service1();

                client.LoginAsync(editUsername.Text, editPassword.Text);

                client.LoginCompleted += client_LoginCompleted;
            }

            else
            {
                return;
            }

        }

        private void client_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            string loginAccepted = e.Result;


            if (loginAccepted == "1")
            {
                var intent = new Intent(this, typeof(Home));
                //intent.PutExtra("Username", editUsername.Text);
                //intent.PutExtra("Password", editPassword.Text);
                StartActivity(intent);

            }

            else
            {
                Toast.MakeText(this, "Please try again!", ToastLength.Long).Show();
            }
        }
    }
}

