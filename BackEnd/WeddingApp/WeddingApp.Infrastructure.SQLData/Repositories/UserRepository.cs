using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WeddingApp.Core.DomainService;
using WeddingApp.Entity;

namespace WeddingApp.Infrastructure.SQLData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _ctx;

        public UserRepository(DBContext context)
        {
            _ctx = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _ctx.Users;
        }

        public void UpdateUser(User user)
        {
            _ctx.Attach(user).State = EntityState.Modified;
            _ctx.SaveChanges();
        }
    }
}
