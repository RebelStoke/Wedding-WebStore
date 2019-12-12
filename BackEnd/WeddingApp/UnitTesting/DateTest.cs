using Moq;
using System;
using System.IO;
using WeddingApp.Core.ApplicationService;
using WeddingApp.Core.ApplicationService.ImplementedService;
using WeddingApp.Core.DomainService;
using Xunit;

namespace UnitTesting
{
    public class DateTest
    {

        [Theory]
        [InlineData(1700, 8)]
        [InlineData(2019, 0)]
        [InlineData(2100, 0)]
        [InlineData(2100, 12)]
        [InlineData(2000, 13)]
        public void GetAllDatesForMonth_ThrowsInvalidDataException(int year, int month)
        {

            Mock<IDateRepository> dateRepository = new Mock<IDateRepository>();

            IDateService dateService = new DateService(dateRepository.Object);

            //Assert
            Assert.Throws<InvalidDataException>((Action)(() => dateService.GetAllDatesForMonth(year, month)));
        }


    }
}
