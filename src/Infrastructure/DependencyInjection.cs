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
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            // Debug logging for Render
            Console.WriteLine($"DEBUG: ConnectionString found: {!string.IsNullOrEmpty(connectionString)}");
            if (!string.IsNullOrEmpty(connectionString))
            {
                // Print start of string to verify format without leaking secrets
                var debugPrefix = connectionString.Length > 10 ? connectionString.Substring(0, 10) : connectionString;
                Console.WriteLine($"DEBUG: ConnectionString starts with: {debugPrefix}...");
            }

            // Handle Render's postgres:// URL format
            if (!string.IsNullOrEmpty(connectionString) && 
               (connectionString.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) || 
                connectionString.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase)))
            {
                try 
                {
                    var databaseUri = new Uri(connectionString);
                    var userInfo = databaseUri.UserInfo.Split(':');
                    var builder = new Npgsql.NpgsqlConnectionStringBuilder
                    {
                        Host = databaseUri.Host,
                        Port = databaseUri.Port > 0 ? databaseUri.Port : 5432,
                        Username = userInfo.Length > 0 ? userInfo[0] : string.Empty,
                        Password = userInfo.Length > 1 ? userInfo[1] : string.Empty,
                        Database = databaseUri.LocalPath.TrimStart('/'),
                        SslMode = Npgsql.SslMode.Require,
                        TrustServerCertificate = true
                    };
                    connectionString = builder.ToString();
                    Console.WriteLine("DEBUG: Successfully parsed postgres URL to Npgsql format.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"DEBUG: Failed to parse postgres URL: {ex.Message}");
                }
            }

            options.UseNpgsql(connectionString);
        });

        // Add Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IGiftRepository, GiftRepository>();

        return services;
    }
}
