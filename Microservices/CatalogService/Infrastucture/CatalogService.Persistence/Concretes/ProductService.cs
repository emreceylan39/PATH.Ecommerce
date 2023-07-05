using AutoMapper;
using CatalogService.Application.Abstractions;
using CatalogService.Domain.DbContexts;
using CatalogService.Domain.DTOs.Product;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        private readonly CatalogServiceDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(CatalogServiceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Product> AddProduct(ProductDtoForInsertion productDto)
        {
            await _context.Products.AddAsync(_mapper.Map<Product>(productDto));
            await _context.SaveChangesAsync();

            return _mapper.Map<Product>(productDto);
        }

        public async Task<List<Product>> GetAllProducts(bool trackChanges)
        {
            return await _context.Products.Include(p=>p.Category).ToListAsync();
        }

        public async Task<Product?> GetProductById(bool trackChanges, int id)
        {
            return trackChanges
                ? await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(c => c.Id == id)
                : await _context.Products.Include(p => p.Category).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool RemoveProduct(int id)
        {
            Product? product = _context.Products.Find(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
    }
}
