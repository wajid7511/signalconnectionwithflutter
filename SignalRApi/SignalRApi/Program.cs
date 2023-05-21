using Microsoft.OpenApi.Models;
using SignalRApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCoreDepencies();
builder.Services.AddSwaggerDepencies();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseSwaggerConfiguration();
app.UseAuthorization();
app.MapControllers();
app.MapHubs();
app.Run();

