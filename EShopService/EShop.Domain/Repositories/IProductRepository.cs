﻿using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Repositories;

public interface IProductRepository
{
    #region Product
    Task<Product> GetProductAsync(int id);
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    //Task<Product> PatchProductAsync(Product product);
    Task<List<Product>> GetAllProductAsync();
    #endregion

}
