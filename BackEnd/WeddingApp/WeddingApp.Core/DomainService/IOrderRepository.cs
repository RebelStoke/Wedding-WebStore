using System;
using System.Collections.Generic;
using System.Text;
using WeddingApp.Entity;
using WeddingApp.Entity.Filters;

namespace WeddingApp.Core.DomainService
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order orderToCreate);
        Order DeleteOrder(Order ord);
        IEnumerable<Order> ReadAllOrders(Filter filter = null);
        Order ReadById(int orderID);
        Order EditOrder(Order orderToEdit);
        IEnumerable<DateTime> GetAllDates(int month);
    }
}
