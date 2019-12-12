using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using WeddingApp.Core.DomainService;
using WeddingApp.Entity;

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

        public void ValidateToken(string username, string tokenToValidate)
        {
            var user = CheckForValidUser(username);

            if (user.RefreshToken == null || tokenToValidate != user.RefreshToken)
            {
               throw new SecurityTokenException("Invalid refresh token");
            }

        }

        public void SaveRefreshToken(string username, string refreshToSave)
        {
            var user = CheckForValidUser(username);

            user.RefreshToken = refreshToSave;

            _userRepo.UpdateUser(user);
        }

        public Tuple<string, string> ValidateUser(Tuple<string, string> attemptAtLogin)
        {
           var user = CheckForValidUser(attemptAtLogin.Item1);

            if (!_authentication.VerifyPasswordHash(attemptAtLogin.Item2, user.PasswordHash, user.PasswordSalt))
            {
                throw new ArgumentException("Invalid password");
            }

            var claims = SetUpClaims(user);

            //Generate refresh token and save it for user.

            var generatedToken = _authentication.GenerateRefreshToken();

            SaveRefreshToken(user.Username, generatedToken);

            return new Tuple<string, string>(_authentication.GenerateToken(claims), generatedToken);
        }

        private List<Claim> SetUpClaims(User user)
        {
            //Generate claims for token
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username)
                    };

            if (user.IsAdmin) claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            else claims.Add(new Claim(ClaimTypes.Role, "User"));

            return claims;
        }

        private User CheckForValidUser(String username)
        {
            var user = _userRepo.GetUserByUsername(username);

            if (user == null)
            {
                throw new ArgumentException("Invalid User");
            }

            return user;
        }

        public Tuple<string, string> RefreshAndValidateToken(Tuple<string, string> attemptAtRefresh)
        {
            //Validates Exisitng info
            var principal = _authentication.GetExpiredPrincipal(attemptAtRefresh.Item1);
            ValidateToken(principal.Identity.Name, attemptAtRefresh.Item2);

            //Creates new token
            var newJwtToken = _authentication.GenerateToken(principal.Claims);
            var newRefreshToken = _authentication.GenerateRefreshToken(); 

            //Saves token
            SaveRefreshToken(principal.Identity.Name, newRefreshToken); 

            return new Tuple<string, string>(newJwtToken, newRefreshToken);
        }
    }
}