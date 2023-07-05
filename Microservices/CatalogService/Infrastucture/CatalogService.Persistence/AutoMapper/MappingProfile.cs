using AutoMapper;
using CatalogService.Domain.DTOs.Category;
using CatalogService.Domain.DTOs.Product;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Persistence.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDtoForInsertion, Category>().ReverseMap();
            CreateMap<Category, CategoryDtoForInsertion>().ReverseMap();

            CreateMap<ProductDtoForInsertion, Product>().ReverseMap();
            CreateMap<Product, ProductDtoForInsertion>().ReverseMap();
        }
    }
}
