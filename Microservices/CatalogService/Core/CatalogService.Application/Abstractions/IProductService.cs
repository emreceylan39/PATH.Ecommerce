using CatalogService.Domain.DTOs.Product;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Abstractions
{
    public interface IProductService
    {
        Task<Product> AddProduct(ProductDtoForInsertion productDto);
        bool RemoveProduct(int id);
        Task<List<Product>> GetAllProducts(bool trackChanges);
        Task<Product> GetProductById(bool trackChanges, int id);
    }
}
