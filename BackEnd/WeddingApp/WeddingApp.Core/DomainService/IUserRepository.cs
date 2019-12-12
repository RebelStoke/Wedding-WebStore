using System.Collections.Generic;
using WeddingApp.Entity;

namespace WeddingApp.Core.DomainService
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User GetUserByUsername(string username);
        void UpdateUser(User user);

        User CreateUser(User user);
    }
}