using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Application;

public interface IProductService
{
    public Task<List<Product>> GetAllAsync();
    Task<Product> GetAsync(int id);
    Task<Product> Update(Product product);
    Task<Product> Add(Product product);
}