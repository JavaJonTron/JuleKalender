using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        // Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Handle Render's postgres:// URL format
            if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("postgres://"))
            {
                var databaseUri = new Uri(connectionString);
                var userInfo = databaseUri.UserInfo.Split(':');
                var builder = new Npgsql.NpgsqlConnectionStringBuilder
                {
                    Host = databaseUri.Host,
                    Port = databaseUri.Port,
                    Username = userInfo[0],
                    Password = userInfo[1],
                    Database = databaseUri.LocalPath.TrimStart('/'),
                    SslMode = Npgsql.SslMode.Require,
                    TrustServerCertificate = true // For Render's self-signed certs if needed
                };
                connectionString = builder.ToString();
            }

            options.UseNpgsql(connectionString);
        });

        // Add Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IGiftRepository, GiftRepository>();

        return services;
    }
}
