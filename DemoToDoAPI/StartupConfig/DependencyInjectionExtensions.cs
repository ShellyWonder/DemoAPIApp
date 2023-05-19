using DemoAPIClassLibrary.SQLDataAccess;
using DemoToDoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DemoToDoAPI.StartupConfig;
//creates a static class to hold the extension methods
public static class DependencyInjectionExtensions
{
    public static void AddStandardServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
    }
    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IToDoData, ToDoData>();
        builder.Services.AddScoped<ITokenService, TokenService>();

    }

    public static void AddAuthenticationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(opts =>
        {
            //Requires all endpoints to be secure unless otherwise noted;
            opts.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
        });

        builder.Services.AddAuthentication("Bearer")
          .AddJwtBearer(opts =>
          {
              opts.TokenValidationParameters = new()
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
                  ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
                  IssuerSigningKey = new SymmetricSecurityKey
                                  (Encoding.ASCII.GetBytes
                                  (builder.Configuration.GetValue<string>
                                  ("Authentication:SecretKey") ?? "DefaultSecretKey"))
              };
          });
    }
    public static void AddHealthCheckServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));


    }
}
