using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WeddingApp.Core.DomainService;
using WeddingApp.Entity;
using WeddingApp.Entity.Filters;

namespace WeddingApp.Core.ApplicationService.ImplementedService
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public Order CreateOrder(Order orderToCreate)
        {
            //Check for valid info
            return _orderRepo.CreateOrder(orderToCreate);
        }

        public Order DeleteOrder(int orderID)
        {
            Order ord = ReadByID(orderID);
            if (ord == null)
            {
                throw new ArgumentException("Owner does not exist");
            }
            return _orderRepo.DeleteOrder(ord);
        }

        public Order EditOrder(Order orderToEdit)
        {
            //Check for valid info
            return _orderRepo.EditOrder(orderToEdit);
        }

        public List<DateTime> GetAllDates(int month)
        {
            if (month < 0 && month > 12)
            {
                throw new InvalidDataException("Month is invalid");
            }
            return _orderRepo.GetAllDates(month).ToList();
        }

        public List<Order> GetAllOrders(Filter filter = null)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPerPage < 0)
            {
                throw new InvalidDataException("Current page cannot be negetive. Items per page cannot be negetive");
            }
            return _orderRepo.ReadAllOrders(filter).ToList();
        }

        public Order ReadByID(int orderID)
        {
            if (orderID < 0)
            {
                throw new ArgumentException("Id cannot be negetive");
            }
            return _orderRepo.ReadById(orderID);
        }
    }
}
