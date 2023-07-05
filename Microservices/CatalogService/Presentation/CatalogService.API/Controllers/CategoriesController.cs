using CatalogService.Application.Abstractions;
using CatalogService.Domain.DTOs.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetCategories()
        {

            return Ok(await _categoryService.GetAllCategories(false));
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(await _categoryService.GetCategoryById(trackChanges: false, id: id));
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> AddCategories(CategoryDtoForInsertion categoryDto)
        {
            return Ok(await _categoryService.AddCategory(categoryDto));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            return Ok(_categoryService.RemoveCategory(id));
        }
    }
}
