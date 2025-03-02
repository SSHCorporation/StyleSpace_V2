using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Entities
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
