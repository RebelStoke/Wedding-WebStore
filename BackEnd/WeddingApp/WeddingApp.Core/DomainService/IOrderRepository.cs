using System.Collections.Generic;
using WeddingApp.Entity;
using WeddingApp.Entity.Filters;

namespace WeddingApp.Core.DomainService
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order orderToCreate);

        Order DeleteOrder(int ord);

        IEnumerable<Order> ReadAllOrders(Filter filter = null);

        Order ReadById(int orderID);

        Order EditOrder(Order orderToEdit);
    }
}