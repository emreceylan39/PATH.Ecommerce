using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.DTOs.Product
{
    public class ProductDtoForInsertion
    {
        public int CategoryId { get; init; }
        public string Name { get; init; }
        public double Price { get; init; }
        public int UnitsInStock { get; init; }
    }
}
