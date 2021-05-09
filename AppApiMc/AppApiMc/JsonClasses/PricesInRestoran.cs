using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppApiMc.JsonClasses
{
    class PricesInRestoran
    {
        public itemsPriceRest[] items { get; set; }
    }
    public class itemsPriceRest
    {
        public int productId { get; set; }
        public double price { get; set; }
    }
}
