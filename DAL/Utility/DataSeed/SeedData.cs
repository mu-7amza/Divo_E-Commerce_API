using DAL.Contexts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL.Utility.DataSeed
{
    public class SeedData
    {
        public static async Task SeedDatabaseAsynce(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../DAL/Utility/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }

            if (!context.Categories.Any())
            {
                var categoryData = File.ReadAllText("../DAL/Utility/DataSeed/Categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);
                context.Categories.AddRange(categories);
            }

            if (!context.Brands.Any())
            {
                var brandData = File.ReadAllText("../DAL/Utility/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<Brand>>(brandData);
                context.Brands.AddRange(brands);
            }

            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}
