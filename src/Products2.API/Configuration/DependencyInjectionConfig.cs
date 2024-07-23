using Microsoft.EntityFrameworkCore;
using Products2.API.Application.Services;
using Products2.API.Data;

namespace Products2.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase("CatalogDB"));

        services.AddTransient<ProductPopulateService>();

        return services;
    }
}
