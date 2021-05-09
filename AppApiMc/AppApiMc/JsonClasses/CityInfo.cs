using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppApiMc.JsonClasses
{
    class CityInfo
    {
        public int lastUpdated { get; set; }
        public items[] items { get; set; }
    }
    public class items
    {
        public string region { get; set; }
        public string name { get; set; }
        public string timezone { get; set; }
        public string id { get; set; }
    }
}
