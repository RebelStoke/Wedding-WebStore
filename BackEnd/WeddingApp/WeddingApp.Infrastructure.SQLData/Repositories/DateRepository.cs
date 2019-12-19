using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WeddingApp.Core.DomainService;
using WeddingApp.Entity;

namespace WeddingApp.Infrastructure.SQLData.Repositories
{
    public class DateRepository : IDateRepository
    {
        private readonly DBContext _context;

        public DateRepository(DBContext context)
        {
            _context = context;
        }

        public IEnumerable<DatesAssigned> GetAllDatesAssigned()
        {
            return _context.Dates.AsNoTracking();
        }

        public IEnumerable<DatesAssigned> GetAllDatesForMonth(int year, int month)
        {
            if (month == 12)
            {
                return _context.Dates.Where(x => x.TakenDate >= DateTime.Parse(month.ToString() + "/01/" + year.ToString()) && x.TakenDate < DateTime.Parse("01/01/" + (year + 1).ToString())).Include(o => o.Order);
            }
            else
            {
                return _context.Dates.Where(x => x.TakenDate >= DateTime.Parse(month.ToString() + "/01/" + year.ToString()) && x.TakenDate < DateTime.Parse((month + 1).ToString() + "/01/" + year.ToString())).Include(o => o.Order);
            }
        }
    }
}