using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WeddingApp.Core.ApplicationService;
using WeddingApp.Core.ApplicationService.HelperService;
using WeddingApp.Core.ApplicationService.ImplementedService;
using WeddingApp.Core.DomainService;
using WeddingApp.Entity;
using WeddingApp.Entity.Filters;
using Xunit;

namespace UnitTesting
{
    public class OrderTest
    {
        [Fact]
        public void ReadyByID_ValidData()
        {

            Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();
            int Id = 1;
            Order ord1 = new Order()
            {
                ID = Id,
                DateWhenOrderWasMade = new DateObject(){day = 5, month = 9, year = 2019},
                Customer = new Customer() { Name = "Son", Phone = "6459124429", Email = "dragonballz@gmail.com" },
                Location = "crusty",
                DateForOrderToBeCompleted = new DateObject(){day = 9, month = 9, year = 2019},
                Type = OrderApprovalType.Approved
            };


            orderRepository.Setup(repo => repo.ReadById(Id)).Returns(ord1);

            IOrderService orderService = new OrderService(orderRepository.Object);


            var actual = orderService.ReadByID(Id);
            Assert.Equal(ord1, actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(12)]
        public void ReadyByID_ThrowsArgumentException(int id)
        {

            Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();
            int Id = 0;
            Order ord1 = new Order()
            {
                ID = 1,
                DateWhenOrderWasMade = new DateObject(){day = 5, month = 9, year = 2019},
                Customer = new Customer() { Name = "Son", Phone = "6459124429", Email = "dragonballz@gmail.com" },
                Location = "crusty",
                DateForOrderToBeCompleted = new DateObject(){day = 9, month = 9, year = 2019},
                Type = OrderApprovalType.Approved
            };


            orderRepository.Setup(repo => repo.ReadById(Id)).Returns(ord1);

            IOrderService orderService = new OrderService(orderRepository.Object);

            //Assert
            Assert.Throws<ArgumentException>((Action)(() => orderService.ReadByID(id)));
        }



        [Fact]
        public void DeleteByID_ValidData()
        {

            Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();
            int Id = 1;
            Order ord1 = new Order()
            {
                ID = Id,
                DateWhenOrderWasMade = new DateObject(){day = 5, month = 9, year = 2019},
                Customer = new Customer() { Name = "Son", Phone = "6459124429", Email = "dragonballz@gmail.com" },
                Location = "crusty",
                DateForOrderToBeCompleted = new DateObject(){day = 9, month = 9, year = 2019},
                Type = OrderApprovalType.Approved
            };


            orderRepository.Setup(repo => repo.DeleteOrder(Id)).Returns(ord1);

            IOrderService orderService = new OrderService(orderRepository.Object);


            var actual = orderService.DeleteOrder(Id);
            Assert.Equal(ord1, actual);
        }


        [Theory]
        [InlineData(-1,15)]
        [InlineData(2, -15)]
        [InlineData(-2, -15)]
        public void ReadAll_NegetiveFilter(int currentpage, int pageCount )
        {

            Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();

            IOrderService orderService = new OrderService(orderRepository.Object);

            //Assert
            Assert.Throws<ArgumentException>((Action)(() => orderService.GetAllOrders(new Filter() { CurrentPage = currentpage, ItemsPerPage = pageCount })));
        }

        [Fact]
        public void CreateOrder_ValidData()
        {

            Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();
            int Id = 1;
            Order ord1 = new Order()
            {
                ID = Id,
                DateWhenOrderWasMade = new DateObject(){day = 5, month = 9, year = 2019},
                Customer = new Customer() { Name = "Son", Phone = "6459124429", Email = "dragonballz@gmail.com" },
                Location = "crusty",
                DateForOrderToBeCompleted = new DateObject(){day = 9, month = 9, year = 2019},
                Type = OrderApprovalType.Approved
            };


            orderRepository.Setup(repo => repo.CreateOrder(ord1)).Returns(ord1);

            IOrderService orderService = new OrderService(orderRepository.Object);


            var actual = orderService.CreateOrder(ord1);
            Assert.Equal(ord1, actual);
        }


        [Fact]
        public void CreateOrder_InValidData_CustomerExceptions()
        {

            Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();
            int Id = 1;
            Order ord1 = new Order()
            {
                ID = Id,
                DateWhenOrderWasMade = new DateObject(){day = 5, month = 9, year = 2019},
                Customer = new Customer() { Name = "ASd",  Email = "dragonballz@gmail.com" },
                Location = "crusty",
                DateForOrderToBeCompleted = new DateObject(){day = 9, month = 9, year = 2019},
                Type = OrderApprovalType.Approved
            };


            orderRepository.Setup(repo => repo.CreateOrder(ord1)).Returns(ord1);

            IOrderService orderService = new OrderService(orderRepository.Object);


            Assert.Throws<ArgumentException>((Action)(() => orderService.CreateOrder(ord1)));
        }

        [Fact]
        public void CreateOrder_InValidData_DateExceptions()
        {

            Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();
            int Id = 1;
            Order ord1 = new Order()
            {
                ID = Id,
                DateWhenOrderWasMade = new DateObject(),
                Customer = new Customer() { Name = "Son", Phone = "6459124429", Email = "dragonballz@gmail.com" },
                Location = "crusty",
                DateForOrderToBeCompleted = new DateObject(){day = 9, month = 9, year = 2019},
                Type = OrderApprovalType.Approved
            };


            orderRepository.Setup(repo => repo.CreateOrder(ord1)).Returns(ord1);

            IOrderService orderService = new OrderService(orderRepository.Object);


            Assert.Throws<ArgumentException>((Action)(() => orderService.CreateOrder(ord1)));
        }

        [Fact]
        public void CreateOrder_InValidData_MissingCustomerExceptions()
        {

            Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();
            int Id = 1;
            Order ord1 = new Order()
            {
                ID = Id,
                DateWhenOrderWasMade = new DateObject(){day = 5, month = 9, year = 2019},
                Location = "crusty",
                DateForOrderToBeCompleted = new DateObject(){day = 9, month = 9, year = 2019},
                Type = OrderApprovalType.Approved

            };


            orderRepository.Setup(repo => repo.CreateOrder(ord1)).Returns(ord1);

            IOrderService orderService = new OrderService(orderRepository.Object);


            Assert.Throws<ArgumentException>((Action)(() => orderService.CreateOrder(ord1)));
        }


        [Fact]
        public void EditOrder_ValidData()
        {

            Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();
            int Id = 1;
            Order given = new Order()
            {
                ID = Id,
                DateWhenOrderWasMade = new DateObject(){day = 5, month = 9, year = 2019},
                Customer = new Customer() { Name = "Son", Phone = "6459124429", Email = "dragonballz@gmail.com" },
                Location = "crusty",
                DateForOrderToBeCompleted = new DateObject(){day = 9, month = 9, year = 2019},
                Type = OrderApprovalType.Approved
            };

            Order expected = new Order()
            {
                ID = Id,
                DateWhenOrderWasMade = new DateObject(){day = 5, month = 9, year = 2019},
                Customer = new Customer() { Name = "Son", Phone = "6459124429", Email = "dragonballz@gmail.com" },
                Location = "crustys",
                DateForOrderToBeCompleted = new DateObject(){day = 9, month = 9, year = 2019},
                Type = OrderApprovalType.Approved
            };
            orderRepository.Setup(repo => repo.ReadById(Id)).Returns(given);
            orderRepository.Setup(repo => repo.EditOrder(given)).Returns(expected);

            IOrderService orderService = new OrderService(orderRepository.Object);


            var actual = orderService.EditOrder(given);
            Assert.Equal(expected, actual);
        }
    }
}
