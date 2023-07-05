using System;
using System.Runtime.CompilerServices;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using SignalR.Core;
using SignalR.Core.Abstraction;
using SignalR.Database;
using SignalR.Database.DAL;
using SignalRApi.Core;

namespace SignalRApi.Extensions
{
    public static class ServerCollectionExtensions
    {
        public static IServiceCollection AddCoreDepencies(this IServiceCollection services)
        {
            services.AddSingleton<NotifyHub>();
            services.AddScoped<IChatManager, ChatManager>();
            return services;
        }
        public static IServiceCollection AddDatabaseDepencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDatabaseOptions>(configuration.GetSection(MongoDatabaseOptions.CONFIGPATH));

            services.AddScoped<ChatDAL>();

            return services;
        }
        public static IServiceCollection AddSwaggerDepencies(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
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
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }});
            });
            return services;
        }
        public static WebApplication MapHubs(this WebApplication app)
        {
            app.MapHub<NotifyHub>("/chathub");
            return app;
        }
        public static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }
            return app;
        }
    }
}

