using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
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
        SeedData(context);
    }

    private static void SeedData(ApplicationDbContext context)
    {
        var categories = context.Categories.ToList();
        if (categories.Count == 0)
        {
            var categoryList = new List<Category>
            {
                new Category() {Name = "Smart Phone"},
                new Category() {Name = "Computer"},
                new Category() {Name = "Gaming Console"}
            };
            context.Categories.AddRange(categoryList);
            context.SaveChanges();
        }

        var products = context.Products.ToList();
        if (products.Count == 0)
        {
            var smartPhoneCategory = context.Categories.FirstOrDefault(x => x.Name == "Smart Phone");
            var gamingConsoleCategory = context.Categories.FirstOrDefault(x => x.Name == "Gaming Console");
            var productList = new List<Product>
            {
                new Product()
                {
                    Name = "iPhone 13 Pro",
                    Price = 999,
                    Code = "iphone13pro",
                    Images = new[]
                    {
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Apple_logo_black.svg/150px-Apple_logo_black.svg.png"
                    },
                    Stock = 20,
                    Category = smartPhoneCategory
                },
                new Product()
                {
                    Name = "iPhone 13 Pro Max",
                    Price = 1099,
                    Code = "iphone13pro",
                    Images = new[]
                    {
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Apple_logo_black.svg/150px-Apple_logo_black.svg.png"
                    },
                    Stock = 10,
                    Category = smartPhoneCategory
                },
                new Product()
                {
                    Name = "Playstation 5 Digital Edition",
                    Price = 399.99,
                    Code = "ps5digital",
                    Images = new[] {"https://upload.wikimedia.org/wikipedia/commons/0/00/PlayStation_logo.svg"},
                    Stock = 100,
                    Category = gamingConsoleCategory
                },
                new Product()
                {
                    Name = "Playstation 5",
                    Price = 499.99,
                    Code = "ps5",
                    Images = new[] {"https://upload.wikimedia.org/wikipedia/commons/0/00/PlayStation_logo.svg"},
                    Stock = 50,
                    Category = gamingConsoleCategory
                }
            };
            context.Products.AddRange(productList);
            context.SaveChanges();
        }
    }
}