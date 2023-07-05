using AutoMapper;
using CatalogService.Application.Abstractions;
using CatalogService.Domain.DbContexts;
using CatalogService.Domain.DTOs.Category;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Persistence.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly CatalogServiceDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(CatalogServiceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Category> AddCategory(CategoryDtoForInsertion categoryDto)
        {
            await _context.Categories.AddAsync(_mapper.Map<Category>(categoryDto));
            await _context.SaveChangesAsync();

            return _mapper.Map<Category>(categoryDto);
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public async Task<List<Category>> GetAllCategories(bool trackChanges)
        {
            return trackChanges 
                ? await _context.Categories.Include(c=>c.Products).ToListAsync() 
                : await _context.Categories.AsNoTracking().Include(c => c.Products).ToListAsync();
        }

        public async Task<Category?> GetCategoryById(bool trackChanges, int id)
        {
            return trackChanges 
                ? await _context.Categories.Include(c=>c.Products).FirstOrDefaultAsync(c=>c.Id==id) 
                : await _context.Categories.Include(c=>c.Products).AsNoTracking().FirstOrDefaultAsync(c=> c.Id==id);
        }

        public bool RemoveCategory(int id)
        {
            Category? category = _context.Categories.Find(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }

      
    }
}
