using System.Collections.Generic;
using WeddingApp.Entity;

namespace WeddingApp.Core.DomainService
{
    public interface IDateRepository
    {
        IEnumerable<DatesAssigned> GetAllDatesForMonth(int year, int month);

        IEnumerable<DatesAssigned> GetAllDatesAssigned();
    }
}