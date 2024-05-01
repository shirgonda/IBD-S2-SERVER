using System;
using System.Collections.Generic;
using finalproj.BL;

namespace finalproj.BL
{
    public class CalendarEvent
    {
        private int eventId;
        private int userId;
        private DateTime startTime; 
        private DateTime endTime;   
        private string name;
        private string location;
        private string repit;
        private int day;
        private int month;
        private int year;

        public static List<CalendarEvent> CalendarEvents = new List<CalendarEvent>();

        public CalendarEvent(int userId, DateTime startTime, DateTime endTime, string name, string location, string repit, int day, int month, int year)
        {
            UserId = userId;
            StartTime = startTime;
            EndTime = endTime;
            Name = name;
            Location = location;
            Repit = repit;
            Day = day;
            Month = month;
            Year = year;
        }

        public CalendarEvent() { }

        public int EventId { get => eventId; set => eventId = value; }
        public int UserId { get => userId; set => userId = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime EndTime { get => endTime; set => endTime = value; }
        public string Name { get => name; set => name = value; }
        public string Location { get => location; set => location = value; }
        public string Repit { get => repit; set => repit = value; }
        public int Day { get => day; set => day = value; }
        public int Month { get => month; set => month = value; }
        public int Year { get => year; set => year = value; }


        public bool Insert()
        {
            DBservicesEvent dbs = new DBservicesEvent();
            dbs.Insert(this);
            return true;
        }

        // קריאת כל האירועים עבור משתמש מסוים
        public List<CalendarEvent> Read(int userId)
        {
            DBservicesEvent dbs = new DBservicesEvent();
            return dbs.Read(userId);
        }

        // קריאת אירוע לפי מזהה עבור משתמש מסוים
        public CalendarEvent ReadOne(int eventId, int userId)
        {
            DBservicesEvent dbs = new DBservicesEvent();
            return dbs.ReadOne(eventId, userId);
        }

        public int Update()
        {
            DBservicesEvent dbs = new DBservicesEvent();
            return dbs.Update(this);
        }

        public bool Delete()
        {
            DBservicesEvent dbs = new DBservicesEvent();
            return dbs.Delete(this);
        }
    }
}
    

