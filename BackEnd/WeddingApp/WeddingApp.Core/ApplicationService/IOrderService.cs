using System;
using System.Collections.Generic;
using System.Text;
using WeddingApp.Entity;
using WeddingApp.Entity.Filters;

namespace WeddingApp.Core.ApplicationService
{
   public interface IOrderService
    {
        Order CreateOrder(Order orderToCreate);
        Order EditOrder(Order orderToEdit);
        Order DeleteOrder(int orderID);

        List<Order> GetAllOrders(Filter filter = null);
        List<DateTime> GetAllDates(int month);

        Order ReadByID(int orderID);
        
    }
}
