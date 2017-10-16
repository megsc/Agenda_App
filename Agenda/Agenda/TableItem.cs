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
    class TableItem
    {
        public string MeetingName { get; set; }
        public string MeetingTime { get; set; }
        public string MeetingLocation { get; set; }

        public string ItemName { get; set; }


    }
}