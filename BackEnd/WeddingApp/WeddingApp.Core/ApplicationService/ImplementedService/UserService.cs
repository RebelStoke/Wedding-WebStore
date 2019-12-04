using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WeddingApp.Core.DomainService;

namespace WeddingApp.Core.ApplicationService.ImplementedService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IAuthenticationService _authentication;

        public UserService(IUserRepository userRepo, IAuthenticationService auth)
        {
            _userRepo = userRepo;
            _authentication = auth;
        }
        public string getRefreshToken(string username)
        {
            var user = _userRepo.GetUsers().ToList().FirstOrDefault(u => u.Username == username);
            return user.RefreshToken;
        }

        public void SaveRefreshToken(string username, string refreshToSave)
        {
            var user = _userRepo.GetUsers().ToList().FirstOrDefault(u => u.Username == username);
            user.RefreshToken = refreshToSave;
            _userRepo.UpdateUser(user);
        }
        public Tuple<string, string> ValidateUser(Tuple<string, string> attemptAtLogin)
        {
            var user = _userRepo.GetUsers().ToList().FirstOrDefault(u => u.Username == attemptAtLogin.Item1);

            if (user == null)
            {
                throw new ArgumentException("Invalid User");
            }

            if (!_authentication.VerifyPasswordHash(attemptAtLogin.Item2, user.PasswordHash, user.PasswordSalt))
            {
                throw new ArgumentException("Invalid password");
            }

            //Generate claims for token
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username)
                    };

            if (user.IsAdmin) claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            else claims.Add(new Claim(ClaimTypes.Role, "User"));

            //Generate refresh token and save it for user.

            var generatedToken = _authentication.GenerateRefreshToken();

            SaveRefreshToken(user.Username, generatedToken);

            return new Tuple<string, string>(_authentication.GenerateToken(claims), generatedToken);
        }
    }
}
