using System;

namespace WeddingApp.Core.ApplicationService
{
    public interface IUserService
    {
        Tuple<string, string> ValidateUser(Tuple<string, string> attemptToLogin);

        Tuple<string, string> RefreshAndValidateToken(Tuple<string, string> attemptAtRefresh);
    }
}