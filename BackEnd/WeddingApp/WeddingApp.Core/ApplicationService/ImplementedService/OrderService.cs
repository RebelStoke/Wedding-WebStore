using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            CheckForValidData(orderToCreate);
            return _orderRepo.CreateOrder(orderToCreate);
        }

        public Order DeleteOrder(int orderID)
        {
            return _orderRepo.DeleteOrder(orderID);
        }

        public Order EditOrder(Order orderToEdit)
        {
            CheckForValidData(orderToEdit);
            return _orderRepo.EditOrder(orderToEdit);
        }

        public List<Order> GetAllOrders(Filter filter = null)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPerPage < 0)
            {
                throw new ArgumentException("Current page cannot be negetive. Items per page cannot be negetive");
            }
            return _orderRepo.ReadAllOrders(filter).ToList();
        }

        public Order ReadByID(int orderID)
        {
            if (orderID <= 0)
            {
                throw new ArgumentException("Id cannot be negetive");
            }
          var ord=  _orderRepo.ReadById(orderID);

            if (ord == null)
            {
                throw new ArgumentException("Order not found");
            }

            return ord;
        }

        private void CheckForValidData(Order ord)
        {
            if(ord.Customer == null || ord.DateForOrderToBeCompleted == null)
            {
                throw new ArgumentException("Missing Customer or Date");
            }
            if(ord.DateWhenOrderWasMade == null || ord.DateForOrderToBeCompleted.TakenDate == null)
            {
                throw new ArgumentException("Missing Date");
            }
            ParseDates(ord.DateWhenOrderWasMade);
            ParseDates(ord.DateForOrderToBeCompleted.TakenDate);

            if(ord.Location == "" || ord.Customer.Name == "" || ord.Customer.Phone == "" || ord.Customer.Email == ""|| ord.Location == null || ord.Customer.Name == null || ord.Customer.Phone == null || ord.Customer.Email == null)
            {
                throw new ArgumentException("Missing Customer Info");
            }
            if (ord.Type != OrderApprovalType.Approved && ord.Type != OrderApprovalType.Pending && ord.Type != OrderApprovalType.Rejected )
            {
                throw new ArgumentException("Incorrect Type");
            }


        }

        private void ParseDates(DateTime date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(date);
                var myDate = DateTime.Now;
                var compareDate1 = myDate.AddYears(-10);
                var compareDate2 = myDate.AddYears(+10);
                if(dt< compareDate1 || compareDate2< dt)
                {
                    throw new ArgumentException("Date is invalid");
                }
            }
            catch (FormatException)
            {
                throw new ArgumentException("Date is invalid");
            }
        }
    }
}