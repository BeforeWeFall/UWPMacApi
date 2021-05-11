using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppApiMc.JsonClasses
{
    class Catalog
    {
        public int lastUpdated { get; set; }
        public ItesmsCatalog[] items { get; set; }
    }
    public class ItesmsCatalog
    {
        public string title { get; set; }
        public string description { get; set; }
        public string[] subcategories { get; set; }
        public string[] products { get; set; }
        public string id { get; set; }
    }
}
