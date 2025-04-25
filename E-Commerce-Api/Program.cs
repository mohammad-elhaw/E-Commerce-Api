using Domain.Contracts;
using E_Commerce_Api.Factories;
using E_Commerce_Api.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Data;
using Persistance.Data.DataSeeding;
using Persistance.Repositories;
using Services;
using Services.Contracts;

namespace E_Commerce_Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            builder.Services.Configure<ApiBehaviorOptions>(opts =>
            {
                opts.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationSegment;
            });
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddCoreServices();

            var app = builder.Build();
            app.ConfigureExceptionHandler();
            app.ConfigureStatusCodePages();

            await InitializeDbAsync(app);

            // Configure the HTTP request pipeline.
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        static async Task InitializeDbAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
        }
    }
}
