
using Microsoft.EntityFrameworkCore;
using Multi_Layer.Application.Interfaces;
using Multi_Layer.Application.Mappings;
using Multi_Layer.Application.Services;
using Multi_Layer.Domain.Interfaces;
using Multi_Layer.Infrastructure.Data;
using Multi_Layer.Infrastructure.Repository;

namespace Multi_Layer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            /*            builder.Services.AddDbContext<ApplicationDbContext>(op => {
                            op.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                            });*/

            // db connection getting connectionString from docker compse
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                        ?? Environment.GetEnvironmentVariable("DATABASE_URL");
            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));


            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ensure database creating
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}
