using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Cost { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

        public string ImageUrl { get; set; }
    }
}
