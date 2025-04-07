using EShop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;

namespace EShop.Domain.Seeders;

public class EShopSeeder(DataContext context) : IEShopSeeder
{
    public async Task Seed()
    {
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product { Name = "Product_A", Ean = "1234"},
                new Product { Name = "Product_B", Ean = "5678"},
                new Product { Name = "Product_C", Ean = "9012"}
            };

            context.Products.AddRange(products);
            context.SaveChanges();
            var seededProducts = context.Products.ToList();
            foreach (var product in seededProducts)
            {
                Console.WriteLine($"Seeded product: {product.Name}");


            }
        }
    }
}
