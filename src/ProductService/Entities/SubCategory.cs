using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Entities
{
    [Table("SubCategories")]
    public class SubCategory : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
