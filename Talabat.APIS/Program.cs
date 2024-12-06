using Microsoft.EntityFrameworkCore;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Core.Entites;
using Talabate.Core.Repositories;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configer Services & Services to the Container 
            //
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            //builder.Services.AddScoped<IGenaricRepository<Product>,GenericRepository<Product>>();
            builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenericRepository<>));

            #endregion
            var app = builder.Build();
            #region Update-Database

            //StoreContext dbContext = new StoreContext(); //Invalide
            //await dbContext.Database.MigrateAsync();

            using var Scope = app.Services.CreateScope();
            //Group Of Services LifeTime Scooped
            var Services = Scope.ServiceProvider;
            //Services ItSelf
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = Services.GetRequiredService<StoreContext>();
                //Ask CLR For Creating Object from DbContext Explicitly 
                await dbContext.Database.MigrateAsync(); //Update Database
                await StoreContextSeed.SeedAsync(dbContext);

            }
            catch (Exception ex)
            {

                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An error occurred while migrating the database.");
            }

            #endregion

            #region // Configure the HTTP request pipeline.MiddelWars
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion
            app.Run();


        }
    }
}
