using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using SignalRApi.Extensions;
using SignalRApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCoreDepencies();
builder.Services.AddDatabaseDepencies(builder.Configuration);
builder.Services.AddSwaggerDepencies();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "codemach",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("decodemeifyoucan"))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseSwaggerConfiguration();

app.UseMiddleware<CustomExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHubs();
app.Run();

