using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using WeddingApp.Core.ApplicationService;

namespace WeddingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;

        public LoginController(IAuthenticationService authService, IUserService userService)
        {
            _userService = userService;
            _authService = authService;
        }

        // GET: api/Login
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        // POST: api/Login
        public IActionResult Post([FromBody] JObject data)
        {
            try
            {
                var validatedUser = _userService.ValidateUser(new Tuple<string, string>(data["username"].ToString(), data["password"].ToString()));

                return Ok(new
                {
                    Token = validatedUser.Item1,
                    RefreshToken = validatedUser.Item2
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Login/5
        [HttpPut]
        public IActionResult Refresh([FromBody] JObject data)
        {
            try
            {
                var principal = _authService.getExpiredPrincipal(data["token"].ToString()); //Check if token is formed correctly.
                var username = principal.Identity.Name; //Get username from expired token
                var savedRefreshToken = _userService.getRefreshToken(username); // Get current user refresh token. Preventing user from modifying the token in any way
                if (savedRefreshToken != data["refreshToken"].ToString()) //If not matching. Front end should disconnect user
                    throw new SecurityTokenException("Invalid refresh token");


                var newJwtToken = _authService.GenerateToken(principal.Claims); //Generate new token with same info as expired token. (IsAdmin and Username is contained)
                var newRefreshToken = _authService.GenerateRefreshToken(); // Generate new refresh token. Effectivly starting new seasion

                _userService.SaveRefreshToken(username, newRefreshToken); //Save new generated re

                return Ok(new
                {
                    Token = newJwtToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
