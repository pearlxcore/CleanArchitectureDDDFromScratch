using BuberDinner.Api.Errors;
using BuberDinner.Application;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDinner.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                // Add services to the container.

                // register services
                builder.Services
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration);

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddControllers();
                //builder.Services.AddSingleton<ProblemDetailsFactory, BuberDInnerProblemDetailsFactory>();
            }


            var app = builder.Build();
            {
                // error handler
                app.UseExceptionHandler("/error");
                app.Map("/error", (HttpContext httpContext) =>
                {
                    Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
                    return Results.Problem(detail: exception.Message);
                });
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
}