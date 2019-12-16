using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeddingApp.Core.DomainService;
using WeddingApp.Entity;

namespace WeddingApp.Core.ApplicationService.ImplementedService
{
    public class DateService : IDateService
    {
        private readonly IDateRepository _dateRepo;

        public DateService(IDateRepository dateRepo)
        {
            _dateRepo = dateRepo;
        }

        public List<DatesAssigned> GetAllDatesAssigned()
        {
            return _dateRepo.GetAllDatesAssigned().ToList();
        }

        public List<DatesAssigned> GetAllDatesForMonth(int year ,int month)
        {
            if (month <= 0 || month > 12)
            {
                throw new InvalidDataException("Month is invalid");
            }
            if(year>2050 || year < 1970)
            {
                throw new InvalidDataException("Year is invalid");
            }

            return _dateRepo.GetAllDatesForMonth(year,month).ToList();
        }
    }
}