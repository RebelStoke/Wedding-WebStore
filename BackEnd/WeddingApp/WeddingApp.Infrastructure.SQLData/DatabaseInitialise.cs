using System;
using System.Collections.Generic;
using System.Text;
using WeddingApp.Core.ApplicationService;
using WeddingApp.Entity;

namespace WeddingApp.Infrastructure.SQLData
{
    public class DatabaseInitialise : IDatabaseInitialise
    {
        private IAuthenticationService _authentication;

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



            Customer cust1 = ctx.Customers.Add(new Customer() { name = "Son", phone = "6459124429", email = "dragonballz@gmail.com" }).Entity;
            Customer cust2 = ctx.Customers.Add(new Customer() { name = "Boom", phone = "696969", email = "Dezz@gmail.com" }).Entity;

            Order ord1 = ctx.Orders.Add(new Order() { dateWhenOrderWasMade = DateTime.Parse("05/08/2014"), customer = cust1 }).Entity;
            Order ord2 = ctx.Orders.Add(new Order() { dateWhenOrderWasMade = DateTime.Parse("05/08/2012"),  customer = cust2 }).Entity;
            Order ord3 = ctx.Orders.Add(new Order() { dateWhenOrderWasMade = DateTime.Parse("05/08/2011"), customer = cust2 }).Entity;

            DatesAssigned datA1 = ctx.Dates.Add(new DatesAssigned() { order = ord1,takenDate = DateTime.Parse("01/04/2015"),reason = "Lol" }).Entity;

            DatesAssigned datA2 = ctx.Dates.Add(new DatesAssigned() { order = ord2, takenDate = DateTime.Parse("01/04/2014"), reason = "UMom" }).Entity;

            DatesAssigned datA3 = ctx.Dates.Add(new DatesAssigned() { order = ord3, takenDate = DateTime.Parse("01/04/2013"), reason = "DoinUr" }).Entity;

            cust1.allOrders.Add(ord1);
            cust2.allOrders.Add(ord2);
            cust2.allOrders.Add(ord3);

            ctx.SaveChanges();
        }
    }
}
