using System;
using WeddingApp.Core.ApplicationService;
using WeddingApp.Entity;

namespace WeddingApp.Infrastructure.SQLData
{
    public class DatabaseInitialise : IDatabaseInitialise
    {
        private readonly IAuthenticationService _authentication;

        public DatabaseInitialise(IAuthenticationService authHelper)
        {
            _authentication = authHelper;
        }

        public void SeedDatabase(DBContext ctx)
        {
            // Create two users with hashed and salted passwords
            string password = "Nedass";
            string password2 = "Yoda";

            var (passwordHashUser1, passwordSaltUser1) = _authentication.CreatePasswordHash(password);
            var (passwordHashUser2, passwordSaltUser2) = _authentication.CreatePasswordHash(password2);

            User user1 = new User()
            {
                Username = "Nedas",
                PasswordHash = passwordHashUser1,
                PasswordSalt = passwordSaltUser1,
                RefreshToken = null,
                IsAdmin = true
            };

            User user2 = new User()
            {
                Username = "CBT",
                PasswordHash = passwordHashUser2,
                PasswordSalt = passwordSaltUser2,
                RefreshToken = null,
                IsAdmin = false
            };
            ctx.Users.Add(user1);
            ctx.Users.Add(user2);

            Customer cust1 = ctx.Customers.Add(new Customer() { Name = "Son", Phone = "6459124429", Email = "dragonballz@gmail.com" }).Entity;
            Customer cust2 = ctx.Customers.Add(new Customer() { Name = "Boom", Phone = "696969", Email = "Dezz@gmail.com" }).Entity;

            Order ord1 = ctx.Orders.Add(new Order() { DateForOrderToBeCompleted = new DateObject(){year = 2019, month = 8, day = 2},DateWhenOrderWasMade = new DateObject(){year = 2019, month = 11, day = 2}, Customer = cust1, Location = "crusty", Type = OrderApprovalType.Approved }).Entity;
            Order ord7 = ctx.Orders.Add(new Order() { DateForOrderToBeCompleted = new DateObject(){year = 2019, month = 9, day = 4}, DateWhenOrderWasMade = new DateObject(){year = 2019, month = 12, day = 2}, Customer = cust2, Location = "crustys", Type = OrderApprovalType.Pending }).Entity;
            Order ord6 = ctx.Orders.Add(new Order() { DateForOrderToBeCompleted = new DateObject(){year = 2019, month = 9, day = 5}, DateWhenOrderWasMade = new DateObject(){year = 2019, month = 12, day = 2}, Customer = cust2, Location = "crustys", Type = OrderApprovalType.Pending }).Entity;
            Order ord3 = ctx.Orders.Add(new Order() { DateForOrderToBeCompleted = new DateObject(){year = 2019, month = 10, day = 4},DateWhenOrderWasMade = new DateObject(){year = 2019, month = 11, day = 5}, Customer = cust2, Location = "krab", Type = OrderApprovalType.Rejected }).Entity;
            Order ord4 = ctx.Orders.Add(new Order() { DateForOrderToBeCompleted = new DateObject(){year = 2019, month = 11, day = 5},DateWhenOrderWasMade = new DateObject(){year = 2019, month = 10, day = 6}, Customer = cust2, Location = "krab", Type = OrderApprovalType.Rejected }).Entity;
            Order ord5 = ctx.Orders.Add(new Order() { DateForOrderToBeCompleted = new DateObject(){year = 2019, month = 12, day = 6},DateWhenOrderWasMade = new DateObject(){year = 2019, month = 9, day = 2}, Customer = cust2, Location = "krab", Type = OrderApprovalType.Rejected }).Entity;
            
            cust1.AllOrders.Add(ord1);
            cust2.AllOrders.Add(ord3);
            cust1.AllOrders.Add(ord4);
            cust1.AllOrders.Add(ord5);
            cust1.AllOrders.Add(ord6);
            cust1.AllOrders.Add(ord7);

            ctx.SaveChanges();
        }
    }
}