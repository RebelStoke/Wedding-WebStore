using System;

namespace WeddingApp.Entity
{
    public class DatesAssigned
    {
        public int ID { get; set; }
        public DateTime TakenDate { get; set; }
        public Order Order { get; set; }
        public String Reason { get; set; }
    }
}