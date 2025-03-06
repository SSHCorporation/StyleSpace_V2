using MongoDB.Entities;

namespace SearchService
{
    public class Item : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}