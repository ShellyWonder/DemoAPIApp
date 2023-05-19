using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using DemoToDoAPI.Services;

namespace DemoToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public AuthenticationsController(IConfiguration configuration, ITokenService tokenService)
        {
            _config = configuration;
            _tokenService = tokenService;
        }
        
        [HttpPost("token")]
        [AllowAnonymous]//exception to the fallback policy
        public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
        {
            var user = ValidateCredentials(data);
            if (user is null)
            {
                return Unauthorized();
            }
            string token = GenerateToken(user);
            return Ok(token);
        }

        private string GenerateToken(UserData user)
        {
            var claims = _tokenService.GenerateClaims(user);
            var token = _tokenService.GenerateJwtSecurityToken(claims);

            return token;
        }


        private UserData? ValidateCredentials(AuthenticationData data)
        {
            //NOT PROD CODE--REPLACE WITH CALL TO AUTH SYSTEM
            if (CompareValues(data.Username, "SJWonder") &&
                CompareValues(data.Password, "Abc&123!"))
            {
                return new UserData(1, "Shelly", "Wonder", data.Username);
            }
            if (CompareValues(data.Username, "Batman") &&
                CompareValues(data.Password, "Abc&123!"))
            {
                return new UserData(2, "Bruce", "Wayne", data.Username);
            }

            return null;
        }

        private bool CompareValues(string? actual, string? expected)
        {
            if (actual is not null)
            {
                if (actual.Equals(expected))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
