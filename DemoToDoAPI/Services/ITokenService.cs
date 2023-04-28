using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DemoToDoAPI.Services
{
    public record AuthenticationData(string? Username, string? Password);

    public record UserData(int Id, string? FirstName, string? LastName, string? UserName);
    public interface ITokenService
    {
        public List<Claim> GenerateClaims(UserData user);
        public SymmetricSecurityKey GenerateSecretKey();
        public string GenerateJwtSecurityToken(List<Claim> claims);
    }
}