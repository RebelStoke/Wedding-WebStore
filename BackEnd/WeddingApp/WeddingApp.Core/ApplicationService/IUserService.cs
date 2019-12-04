using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingApp.Core.ApplicationService
{
    public interface IUserService
    {
        Tuple<string, string> ValidateUser(Tuple<string, string> attemptToLogin);
        string getRefreshToken(string username);

        void SaveRefreshToken(string user, string refreshToSave);
    }
}
