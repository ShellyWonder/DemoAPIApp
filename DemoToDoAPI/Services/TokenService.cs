using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace DemoToDoAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public record AuthenticationData(string? Username, string? Password);
        public record UserData(int Id, string? FirstName, string? LastName, string? UserName);

        //place in program.cs equal to IssuerSigningKey
        public SymmetricSecurityKey GenerateSecretKey()
        {
            return new SymmetricSecurityKey
                                (Encoding.ASCII.GetBytes
                                (s: _config.GetValue<string>
                                ("Authentication:SecretKey") ?? "DefaultSecretKey"));
        }

        public List<Claim> GenerateClaims(Services.UserData user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!));
            claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName!));
            claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName!));
            return claims;
        }

        public string GenerateJwtSecurityToken(List<Claim> claims)
        {
            var secretKey = GenerateSecretKey();
            SigningCredentials signingCredentials = new SigningCredentials(secretKey,
                                               SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
             _config.GetValue<string>("Authentication:Issuer"),
             _config.GetValue<string>("Authentication:Audience"),
             claims,
             DateTime.UtcNow,
             DateTime.UtcNow.AddMinutes(1),
             signingCredentials
             );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
