using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Order DeleteOrder(Order ord)
        {
            _context.Attach(ord).State = EntityState.Deleted;
            _context.SaveChanges();
            return ord;
        }

        public Order EditOrder(Order orderToEdit)
        {
            _context.Attach(orderToEdit).State = EntityState.Modified;
            _context.SaveChanges();
            return orderToEdit;
        }

        public IEnumerable<DateTime> GetAllDates(int month)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> ReadAllOrders(Filter filter)
        {
            IEnumerable<Order> filteredOwners;
            if (filter.CurrentPage != 0 && filter.ItemsPerPage != 0)
            {
              return _context.Orders.Include(o => o.customer).Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage).OrderByDescending(c => c.ID);
            }
            else
            {
                filteredOwners = _context.Orders.AsNoTracking().Include(o => o.customer).Include(p => p.dateForOrderToBeCompleted);
            }
            return filteredOwners;
        }

        public Order ReadById(int orderID)
        {
            return _context.Orders.AsNoTracking().Include(o => o.customer).FirstOrDefault(o => o.ID == orderID);
        }
    }
}
