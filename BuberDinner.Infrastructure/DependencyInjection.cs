using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Services;
using BuberDinner.Application.Persistance;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistance;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            services.Configure<JwtSettings>(configurationManager.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserrRepository>();
            return services;
        }
    }
}
