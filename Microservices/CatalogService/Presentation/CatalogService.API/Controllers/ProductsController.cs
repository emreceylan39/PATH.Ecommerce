using AutoMapper;
using CatalogService.Application.Abstractions;
using CatalogService.Domain.DTOs.Category;
using CatalogService.Domain.DTOs.Product;
using CatalogService.Persistence.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts(false));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _productService.GetProductById(trackChanges: false, id: id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDtoForInsertion productDto)
        {

            return Ok(await _productService.AddProduct(productDto));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult RemoveProduct(int id)
        {
            return Ok(_productService.RemoveProduct(id));
        }
        
    }
}
