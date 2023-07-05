
using CatalogService.Domain.DTOs.Category;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Abstractions
{
    public interface ICategoryService
    {
        Task<Category> AddCategory(CategoryDtoForInsertion categoryDto);
        bool RemoveCategory(int id);
        Task<List<Category>> GetAllCategories(bool trackChanges);
        Task<Category> GetCategoryById(bool trackChanges,int id);

    }
}
