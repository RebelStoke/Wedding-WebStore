using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WeddingApp.Core.DomainService;
using WeddingApp.Entity;

namespace WeddingApp.Infrastructure.SQLData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;

        public UserRepository(DBContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.ToList().FirstOrDefault(u => u.Username == username);
        }


        public void UpdateUser(User user)
        {
            _context.Attach(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public User CreateUser(User user)
        {
            _context.Attach(user).State = EntityState.Added;
            _context.SaveChanges();
            return user;
        }
    }
}