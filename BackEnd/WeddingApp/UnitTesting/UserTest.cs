using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WeddingApp.Core.ApplicationService;
using WeddingApp.Core.ApplicationService.HelperService;
using WeddingApp.Core.ApplicationService.ImplementedService;
using WeddingApp.Core.DomainService;
using WeddingApp.Entity;
using Xunit;

namespace UnitTesting
{
    public class UserTest
    {

        [Theory]
        [InlineData("Nedass", "Nedass")] //Incorrect username
        [InlineData("Nedas", "Nedas")] //Incorrect password
        public void ValidateUser_ArgumentException(string name, string password)
        {

            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();


            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            IAuthenticationService authService = new AuthenticationService(secretBytes);

            var (passwordHashUser1, passwordSaltUser1) = authService.CreatePasswordHash("Nedass");

            User user1 = new User()
            {
                Username = "Nedas",
                PasswordHash = passwordHashUser1,
                PasswordSalt = passwordSaltUser1,
                RefreshToken = null,
                IsAdmin = true
            };

            userRepository.Setup(repo => repo.GetUserByUsername(user1.Username)).Returns(user1);

            IUserService userService = new UserService(userRepository.Object, authService);


            Assert.Throws<ArgumentException>((Action)(() => userService.ValidateUser(new Tuple<string, string>(name, password))));
        }


        [Fact]
        public void RefreshAndValidateToken_NonValidRefreshToken()
        {

            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();


            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            IAuthenticationService authService = new AuthenticationService(secretBytes);

            var (passwordHashUser1, passwordSaltUser1) = authService.CreatePasswordHash("Nedass");

            User user1 = new User()
            {
                Username = "Nedas",
                PasswordHash = passwordHashUser1,
                PasswordSalt = passwordSaltUser1,
                RefreshToken = null,
                IsAdmin = true
            };

            userRepository.Setup(repo => repo.GetUserByUsername(user1.Username)).Returns(user1);

            IUserService userService = new UserService(userRepository.Object, authService);
            var validatedUser = userService.ValidateUser(new Tuple<string, string>("Nedas", "Nedass"));

            Assert.Throws<SecurityTokenException>((Action)(() => userService.RefreshAndValidateToken(new Tuple<string, string>(validatedUser.Item1, "InccorectToken"))));
        }
        [Fact]
        public void RefreshAndValidateToken_NonValidToken()
        {

            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();


            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            IAuthenticationService authService = new AuthenticationService(secretBytes);

            var (passwordHashUser1, passwordSaltUser1) = authService.CreatePasswordHash("Nedass");

            User user1 = new User()
            {
                Username = "Nedas",
                PasswordHash = passwordHashUser1,
                PasswordSalt = passwordSaltUser1,
                RefreshToken = null,
                IsAdmin = true
            };

            userRepository.Setup(repo => repo.GetUserByUsername(user1.Username)).Returns(user1);

            IUserService userService = new UserService(userRepository.Object, authService);

            Assert.Throws<ArgumentException>((Action)(() => userService.RefreshAndValidateToken(new Tuple<string, string>("Lol", "InccorectToken"))));
        }

    }
}
