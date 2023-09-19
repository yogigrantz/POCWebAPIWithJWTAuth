using POCWebAPIJWT.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IAuth, Auth>(_ => new Auth(new JWToken(Constants.ClaimType, Constants.Issuer, Constants.Audience, Constants.AuthType), Constants.ExpirationMinutes));
builder.Services.AddSingleton<CustomAuth>(_ => new CustomAuth(new Auth(new JWToken(Constants.ClaimType, Constants.Issuer, Constants.Audience, Constants.AuthType), Constants.ExpirationMinutes)));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
