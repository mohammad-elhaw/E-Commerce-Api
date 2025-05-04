using Domain.Contracts;
using E_Commerce_Api.Factories;
using E_Commerce_Api.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Persistance;
using Services;

namespace E_Commerce_Api
{
    public static class Program
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
            builder.Services.AddCoreServices(builder.Configuration);

            var app = builder.Build();
            app.ConfigureExceptionHandler();
            app.ConfigureStatusCodePages();

            await InitializeDbAsync(app);

            // Configure the HTTP request pipeline.
            app.UseRouting();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }

        static async Task InitializeDbAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await dbInitializer.DataSeed();
            await dbInitializer.IdentityDataSeed();
        }
    }
}
