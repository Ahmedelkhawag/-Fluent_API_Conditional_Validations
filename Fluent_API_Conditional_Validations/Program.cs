
using Fluent_API_Conditional_Validations.Models;
using Fluent_API_Conditional_Validations.Validations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Fluent_API_Conditional_Validations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<MyContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("MyCS")));
            builder.Services.AddValidatorsFromAssemblyContaining<OrderDTOValidator>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
