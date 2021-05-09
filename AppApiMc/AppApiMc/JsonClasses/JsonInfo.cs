using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppApiMc.JsonClasses
{
    class JsonInfo
    {
        public item[] item { get; set; }
    }
    public class item
    {
        public string name { get; set; }
        public request request { get; set; }

    }
    public class request
    {
        public string method { get; set; }
        public header[] header { get; set; }
        public url url { get; set; }
        public Body body { get; set; }
    }
    public class header
    {
        public string key { get; set; }
        public string value { get; set; }
        public string type { get; set; }
    }
    public class Test
    {
        public string Token { get; set; }
    }
    public class url
    {
        public string raw { get; set; }
    }
    public class Body
    {
        public string raw { get; set; }
    }
}
