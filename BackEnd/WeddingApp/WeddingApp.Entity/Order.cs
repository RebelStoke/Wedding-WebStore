using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingApp.Entity
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime dateWhenOrderWasMade { get; set; }

        public DatesAssigned dateForOrderToBeCompleted { get; set; }

        public Customer customer { get; set; }
    }
}
