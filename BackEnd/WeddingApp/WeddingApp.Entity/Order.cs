﻿using System;

namespace WeddingApp.Entity
{
    public enum OrderApprovalType { Approved, Pending, Rejected };

    public class Order
    {
        public int ID { get; set; }
        public DateObject DateWhenOrderWasMade { get; set; }

        public DateObject DateForOrderToBeCompleted { get; set; }

        public Customer Customer { get; set; }

        public String Location { get; set; }

        public OrderApprovalType Type { get; set; }
    }
}