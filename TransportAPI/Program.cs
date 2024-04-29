using TransportAPI.Entities;

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

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetService<TransportSeeder>();
            seeder.Seed();
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapControllers();

            
            app.Run();
        }
    }
}