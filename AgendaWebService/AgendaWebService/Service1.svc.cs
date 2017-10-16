using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;
using MySql.Data.MySqlClient;
using System.Collections;

namespace AgendaWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        [WebMethod]
        public string Login(string editUsername, string editPassword)
        {
            String conString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString"].ConnectionString;

            string result = "";

            using (MySqlConnection cnn = new MySqlConnection(conString))
            {
                cnn.Open();
                String sql = String.Format("SELECT Count(*) FROM login WHERE Username=\"{0}\" AND Password=\"{1}\"", editUsername, editPassword);
                MySqlCommand cmd = new MySqlCommand(sql, cnn);

                result = cmd.ExecuteScalar().ToString();

            }

            return result;
        }


        [WebMethod]
        public void UpdateItemDetails(string notes, string time, string itemName)
        {
            String conString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString"].ConnectionString;


            using (MySqlConnection cnn = new MySqlConnection(conString))
            {
                cnn.Open();
                String sql = String.Format("UPDATE `meetingitem` SET `itemNotes`= \"{0}\" , `actualItemTime` = \"{1}\" WHERE `itemName` = \"{2}\"", notes, time, itemName);
                MySqlCommand cmd = new MySqlCommand(sql, cnn);

                cmd.ExecuteNonQuery();

            }

        }


        [WebMethod]
        public void UpdateMeetingTime(string time, string meetingName)
        {
            String conString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString"].ConnectionString;


            using (MySqlConnection cnn = new MySqlConnection(conString))
            {
                cnn.Open(); 
                String sql = String.Format("UPDATE `meetings` SET `actualMeetingTime`= \"{0}\" WHERE `meetingName` = \"{1}\"", time, meetingName);
                MySqlCommand cmd = new MySqlCommand(sql, cnn);

                cmd.ExecuteNonQuery();

            }

        }




        [WebMethod]
        public string[] LoadMeetings()
        {
            String conString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString"].ConnectionString;

            MySqlDataReader reader;

            string[] meetings;
            int i = 0;
            ArrayList results = new ArrayList();

            using (MySqlConnection cnn = new MySqlConnection(conString))
            {
                cnn.Open();
                String sql = String.Format("SELECT meetingName FROM meetings WHERE meetingDate BETWEEN (CURRENT_DATE) and (CURRENT_DATE + 8)");
                MySqlCommand cmd = new MySqlCommand(sql, cnn);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(reader.GetValue(0).ToString());
                    
                }

                meetings = new string[results.Count];

                foreach (string details in results)
                {
                    meetings[i] = details;
                    i++;
                }

            }

            reader.Close();
            return meetings;

        }


        [WebMethod]
        public string[] LoadMeetingDetails(string selected)
        {
            String conString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString"].ConnectionString;

            MySqlDataReader reader;

            string[] details;
            int i = 0;
            ArrayList results = new ArrayList();

            using (MySqlConnection cnn = new MySqlConnection(conString))
            {
                cnn.Open();
                String sql = String.Format("SELECT meetingLocation, meetingDate, meetingTime, Attendees, Minutes, Absentees FROM `meetings` WHERE meetingName = \"{0}\"", selected);
                MySqlCommand cmd = new MySqlCommand(sql, cnn);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(reader.GetValue(0).ToString());
                    results.Add(reader.GetValue(1).ToString());
                    results.Add(reader.GetValue(2).ToString());
                    results.Add(reader.GetValue(3).ToString());
                    results.Add(reader.GetValue(4).ToString());
                    results.Add(reader.GetValue(5).ToString());


                }

                details = new string[results.Count];

                foreach (string a in results)
                {
                    details[i] = a;
                    i++;
                }

            }

            reader.Close();
            return details;

        }




        [WebMethod]
        public string[] LoadItems(string selected)
        {
            String conString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString"].ConnectionString;

            MySqlDataReader reader;

            string[] items;
            int i = 0;
            ArrayList results = new ArrayList();

            using (MySqlConnection cnn = new MySqlConnection(conString))
            {
                cnn.Open();
                String sql = String.Format("SELECT itemName FROM meetingitem WHERE meetingName = \"{0}\"", selected);
                MySqlCommand cmd = new MySqlCommand(sql, cnn);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(reader.GetValue(0).ToString());
                }

                items = new string[results.Count];

                foreach (string details in results)
                {
                    items[i] = details;
                    i++;
                }

            }

            reader.Close();
            return items;

        }



        [WebMethod]
        public string[] LoadItemDetails(string selected)
        {
            String conString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString"].ConnectionString;

            MySqlDataReader reader;

            string[] details; 
            int i = 0;
            ArrayList results = new ArrayList();

            using (MySqlConnection cnn = new MySqlConnection(conString))
            {
                cnn.Open();
                String sql = String.Format("SELECT itemOwner, itemTime FROM `meetingitem` WHERE itemName = \"{0}\"", selected);
                MySqlCommand cmd = new MySqlCommand(sql, cnn);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(reader.GetValue(0).ToString());
                    results.Add(reader.GetValue(1).ToString());

                }

                details = new string[results.Count];

                foreach (string a in results)
                {
                    details[i] = a;
                    i++;
                }

            }

            reader.Close();
            return details;

        }

    }
}
