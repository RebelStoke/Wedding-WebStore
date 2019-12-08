using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingApp.Entity
{
    public class DatesAssigned
    {
        public int ID { get; set; }
        public DateTime takenDate { get; set; }
        public Order order { get; set; }
        public String reason { get; set; }
    }
}
