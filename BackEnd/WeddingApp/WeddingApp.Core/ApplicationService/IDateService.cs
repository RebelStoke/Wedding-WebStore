using System.Collections.Generic;
using WeddingApp.Entity;

namespace WeddingApp.Core.ApplicationService
{
    public interface IDateService
    {
        List<DatesAssigned> GetAllDatesAssigned();

        List<DatesAssigned> GetAllDatesForMonth(int year,int month);
    }
}