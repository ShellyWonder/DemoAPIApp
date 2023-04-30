using DemoToDoAPI.StartupConfig;
var builder = WebApplication.CreateBuilder(args);

// Services moved to extension methods in StartupConfig
builder.AddStandardServices();
builder.AddCustomServices();
builder.AddAuthenticationServices();    
builder.AddHealthCheckServices();   

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health").AllowAnonymous();

app.Run();
