using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samochody.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public DateTime Bought { get; set; }
        public DateTime Sold { get; set; }
    }
}