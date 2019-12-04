using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace WeddingApp.Core.ApplicationService
{
    public interface IAuthenticationService
    {
        (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password);

        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

        string GenerateToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal getExpiredPrincipal(string token);
    }
}
