using Microsoft.EntityFrameworkCore;
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
            var countryToDelete = _context.Orders.FirstOrDefault(c => c.ID == ord);
            _context.Orders.Remove(countryToDelete);
            _context.SaveChanges();
            return countryToDelete;
        }

        public Order EditOrder(Order orderToEdit)
        {
            _context.Attach(orderToEdit).State = EntityState.Modified;
            _context.SaveChanges();
            return orderToEdit;
        }

        public IEnumerable<Order> ReadAllOrders(Filter filter)
        {
            IEnumerable<Order> filteredOwners;
            if (filter.CurrentPage != 0 && filter.ItemsPerPage != 0)
            {
                return _context.Orders.Include(o => o.Customer).Include(o => o.Customer).Include(p => p.DateForOrderToBeCompleted).Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage).OrderByDescending(c => c.ID);
            }
            else
            {
                filteredOwners = _context.Orders.AsNoTracking().Include(o => o.Customer).Include(p => p.DateForOrderToBeCompleted);
            }
            return filteredOwners;
        }

        public Order ReadById(int orderID)
        {
            return _context.Orders.AsNoTracking().Include(o => o.Customer).Include(p => p.DateForOrderToBeCompleted).FirstOrDefault(o => o.ID == orderID);
        }
    }
}