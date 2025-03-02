using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Entities;

namespace ProductService.Data;

public class DataSeeder
{
    public static void InitData(WebApplication web)
    {
        using var scope = web.Services.CreateScope();
        SeedData(scope.ServiceProvider.GetService<ProductServiceDbContext>());
    }

    private static void SeedData(ProductServiceDbContext context)
    {
        context.Database.Migrate();

        if (context.Products.Any())
        {
            Console.WriteLine("Data already exists, No need to seed data");
            return;
        }
        var categories = new List<Category>
        {
            new Category() { Name = "Category 1" },
            new Category() { Name = "Category 2" },
        };

        var subCategories = new List<SubCategory>
        {
            new SubCategory() { Name = "SubCategory 1", Category = categories[0] },
            new SubCategory() { Name = "SubCategory 2", Category = categories[1] },
            new SubCategory() { Name = "SubCategory 3", Category = categories[0] },
        };

        var products = new List<Product>
        {
            new Product() { Name = "Product 1", Price = 100, Category = categories[0], SubCategories = new List<SubCategory> { subCategories[0], subCategories[2] } },
            new Product { Name = "Product 2", Price = 200, Category = categories[1], SubCategories = new List<SubCategory> { subCategories[1] } }
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}
