using Core.Interfaces;
using Core.Security;
using Core.Services;
using Core.Services.ExternalServices;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // --------------------
            // Database
            // --------------------
            services.AddScoped<IDbConnectionFactory>(_ =>
                new SqlConnectionFactory(
                    configuration.GetConnectionString("DefaultConnection")));


            


            // --------------------
            // Unit of Work
            // --------------------
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // --------------------
            // Security
            // --------------------
            services.AddScoped<JwtTokenGenerator>();
            services.AddScoped<RefreshTokenService>();

            // --------------------
            // Business Services
            // --------------------
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInvoiceService, InvoiceService>();

            return services;
        }
    }
}
