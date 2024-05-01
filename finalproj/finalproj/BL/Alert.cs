using System;
using System.Collections.Generic;
using finalproj.BL;

namespace finalproj.BL
{
    public class Alert
    {
        private int alertId;
        private int eventId;
        private DateTime alertTime;
        private string aname;
        private string arepit;

        public static List<Alert> Alerts = new List<Alert>();

        public Alert(int eventId, DateTime alertTime, string aname, string arepit)
        {
            EventId = eventId;
            AlertTime = alertTime;
            Aname = aname;
            Arepit = arepit;
        }

        public Alert() { }

        public int AlertId { get => alertId; set => alertId = value; }
        public int EventId { get => eventId; set => eventId = value; }
        public DateTime AlertTime { get => alertTime; set => alertTime = value; }
        public string Aname { get => aname; set => aname = value; }
        public string Arepit { get => arepit; set => arepit = value; }

        // Methods for CRUD operations can be added similarly as in the User class
    }
}
