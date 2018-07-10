using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samochody.Models
{
    public class Service
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int CarID { get; set; }

        public virtual Car Car { get; set; }
    }
}