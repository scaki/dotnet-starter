using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Infrastructure.Context;
using CatalogService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? throw new InvalidOperationException();
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(connectionString, m => { m.EnableRetryOnFailure(); }));

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static void UseInfrastructure(this IApplicationBuilder app)
    {
        var serviceProvider = app.ApplicationServices;
        using var serviceScope = serviceProvider.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        context?.Database.Migrate();
    }
}