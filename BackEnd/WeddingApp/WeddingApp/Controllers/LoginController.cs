using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using WeddingApp.Core.ApplicationService;

namespace WeddingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController (IUserService userService)
        {
            _userService = userService;
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
                var refreshedData = _userService.RefreshAndValidateToken(new Tuple<string, string>(data["token"].ToString(), data["refreshToken"].ToString()));

                return Ok(new
                {
                    Token = refreshedData.Item1,
                    RefreshToken = refreshedData.Item2
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}