using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WeddingApp.Core.DomainService;
using WeddingApp.Entity;
using WeddingApp.Entity.Filters;

namespace WeddingApp.Infrastructure.SQLData.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DBContext _context;

        public OrderRepository(DBContext context)
        {
            _context = context;
        }

        public Order CreateOrder(Order orderToCreate)
        {
            _context.Attach(orderToCreate).State = EntityState.Added;
            _context.SaveChanges();
            return orderToCreate;
        }

        public Order DeleteOrder(int ord)
        {

            Order order = ReadById(ord);
            _context.Attach(order).State = EntityState.Deleted;
            _context.SaveChanges();

            return order;
        }

        public Order EditOrder(Order orderToEdit)
        {
            _context.Attach(orderToEdit).State = EntityState.Modified;
            _context.SaveChanges();
            return orderToEdit;
        }

        public Tuple<List<Order>, int> ReadAllOrders(Filter filter)
        {
            List<Order> filteredOwners;
            if (filter.CurrentPage != 0 && filter.ItemsPerPage != 0)
            {
                filteredOwners= _context.Orders.Include(o => o.Customer).Include(p => p.DateForOrderToBeCompleted).OrderByDescending(c => c.ID).Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage).ToList();
            }
            else
            {
                filteredOwners = _context.Orders.AsNoTracking().Include(o => o.Customer).Include(p => p.DateForOrderToBeCompleted).ToList();
            }
            return new Tuple<List<Order>, int>(filteredOwners, _context.Orders.Count());
        }

        public Order ReadById(int orderID)
        {
            return _context.Orders.AsNoTracking().Include(o => o.Customer).Include(p => p.DateForOrderToBeCompleted).FirstOrDefault(o => o.ID == orderID);
        }
    }
}