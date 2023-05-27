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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseSwaggerConfiguration();

app.UseMiddleware<CustomExceptionMiddleware>();

app.UseAuthorization();
app.MapControllers();
app.MapHubs();
app.Run();

