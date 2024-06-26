using System.Reflection;
using System.Text.Json.Serialization;
using TransportAPI.Entities;
using TransportAPI.Middleware;
using TransportAPI.Services;

namespace TransportAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<TransportDbContext>();
            builder.Services.AddScoped<TransportSeeder>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddScoped<IRunService, RunService>();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<ErrorHandllingMiddleware>();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetService<TransportSeeder>();
            seeder.Seed();

            // Configure the HTTP request pipeline.

            app.UseMiddleware<ErrorHandllingMiddleware>();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transport API");
            });

            app.MapControllers();

            
            app.Run();
        }
    }
}