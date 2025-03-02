using System;
using System.Collections.Generic;
using ProductService.EntityDTOs.SubCategory;

namespace ProductService.EntityDTOs.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public List<Guid> SubCategoryIds { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

        // Include SubCategory DTOs
        public List<SubCategoryDto> SubCategories { get; set; }
    }
}