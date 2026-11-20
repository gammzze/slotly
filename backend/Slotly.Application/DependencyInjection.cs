using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Slotly.Application.Services;

namespace Slotly.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddScoped<IServiceCatalogService, ServiceCatalogService>();

        return services;
    }
}
