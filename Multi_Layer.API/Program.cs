
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
            builder.Services.AddDbContext<ApplicationDbContext>(op => {
                op.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                });

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
