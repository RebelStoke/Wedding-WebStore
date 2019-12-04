using System;
using System.Collections.Generic;
using System.Text;
using WeddingApp.Entity;

namespace WeddingApp.Core.DomainService
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        void UpdateUser(User user);
    }
}
