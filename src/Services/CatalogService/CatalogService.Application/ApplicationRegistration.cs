using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(assembly);
        return services;
    }
}