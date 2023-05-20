using Microsoft.OpenApi.Models;
using SignalRApi.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSingleton<NotifyHub>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Signal R with Flutter API",
        Description = "This is developed to connect Mobile app using web api and Signal R",

        Contact = new OpenApiContact
        {
            Name = "Wajid Muhammad",
            Email = "email2wajidkhan@gmail.com"
        }

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
app.MapControllers();
app.MapHub<NotifyHub>("/chathub");
app.Run();

