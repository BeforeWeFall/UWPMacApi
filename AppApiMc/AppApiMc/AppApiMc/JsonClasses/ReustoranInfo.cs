using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppApiMc.JsonClasses
{
    class ReustoranInfo
    {
        public ItemsRest[] items { get; set; }
    }
    public class ItemsRest
    {
        public string address { get; set; }
        public int store_id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public bool babyTable { get; set; }
        public bool openingSoon { get; set; }
        public Breakfast breakfast { get; set; }
        public Halls[] halls { get; set; }
        public bool deleted { get; set; }
        public bool mcauto { get; set; }
        public bool mccafe { get; set; }
        public bool mcexpress { get; set; }
        public bool temporarilyClosed { get; set; }
        public bool tableService { get; set; }
        public Location location { get; set; }
        public string phone { get; set; }
        public bool delivery { get; set; }
        public bool birthdayCelebration { get; set; }
        public bool childrenParty { get; set; }
        public Metro[] metro { get; set; }
        public bool disabilityServices { get; set; }
        public string id { get; set; }
    }

    public class Metro
    {
        public string name { get; set; }
        public string color { get; set; }
        public double distance { get; set; }
    }

    public class Breakfast
    {
        public string open { get; set; }
        public string close { get; set; }
    }
    public class Halls
    {
        public string name { get; set; }
        public Worktime[] worktime { get; set; }
    }
    public class Location
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }
    public class Worktime
    {
        public string name { get; set; }
        public string open { get; set; }
        public string close { get; set; }
        public string startBreak { get; set; }
        public string endBreak { get; set; }
    }
}
