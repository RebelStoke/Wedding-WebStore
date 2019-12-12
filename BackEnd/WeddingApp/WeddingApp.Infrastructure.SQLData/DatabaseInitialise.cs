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

            Order ord1 = ctx.Orders.Add(new Order() { DateWhenOrderWasMade = DateTime.Parse("05/09/2019"), Customer = cust1, Location = "crusty", Type = OrderApprovalType.Approved }).Entity;
            Order ord2 = ctx.Orders.Add(new Order() { DateWhenOrderWasMade = DateTime.Parse("05/08/2019"), Customer = cust2, Location = "crustys", Type = OrderApprovalType.Pending }).Entity;
            Order ord3 = ctx.Orders.Add(new Order() { DateWhenOrderWasMade = DateTime.Parse("04/08/2019"), Customer = cust2, Location = "krab", Type = OrderApprovalType.Rejected }).Entity;
            Order ord4 = ctx.Orders.Add(new Order() { DateWhenOrderWasMade = DateTime.Parse("12/08/2019"), Customer = cust2, Location = "krab", Type = OrderApprovalType.Rejected }).Entity;
            Order ord5 = ctx.Orders.Add(new Order() { DateWhenOrderWasMade = DateTime.Parse("12/08/2019"), Customer = cust2, Location = "krab", Type = OrderApprovalType.Rejected }).Entity;

             ctx.Dates.Add(new DatesAssigned() { Order = ord1, TakenDate = DateTime.Parse("09/09/2019"), Reason = "Lol" });

            ctx.Dates.Add(new DatesAssigned() { Order = ord2, TakenDate = DateTime.Parse("08/08/2019"), Reason = "UMom" });

            ctx.Dates.Add(new DatesAssigned() { Order = ord3, TakenDate = DateTime.Parse("08/07/2019"), Reason = "DoinUr" });

            ctx.Dates.Add(new DatesAssigned() { Order = ord4, TakenDate = DateTime.Parse("12/09/2019"), Reason = "DoinLol" });

            ctx.Dates.Add(new DatesAssigned() { Order = ord5, TakenDate = DateTime.Parse("01/07/2020"), Reason = "DoinKol" });

            cust1.AllOrders.Add(ord1);
            cust2.AllOrders.Add(ord2);
            cust2.AllOrders.Add(ord3);
            cust1.AllOrders.Add(ord4);
            cust1.AllOrders.Add(ord5);

            ctx.SaveChanges();
        }
    }
}