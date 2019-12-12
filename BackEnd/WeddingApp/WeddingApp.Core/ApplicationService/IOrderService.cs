using System.Collections.Generic;
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

        Order ReadByID(int orderID);
    }
}