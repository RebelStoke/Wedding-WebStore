using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingApp.Entity
{
    public class Customer
    {

        public int ID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

        public List<Order> allOrders { get; set; }
    }
}
