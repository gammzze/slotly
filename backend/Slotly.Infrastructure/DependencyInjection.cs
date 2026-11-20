using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slotly.Infrastructure.Persistence;

namespace Slotly.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException(
                "ConnectionStrings:Default ayarı bulunamadı. User Secrets veya ortam değişkeni ile tanımla.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}
