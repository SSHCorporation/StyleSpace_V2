namespace ProductService.EntityDTOs.Product
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public Guid? CategoryId { get; set; }
        public List<Guid> SubCategoryIds { get; set; }
        public string ImageUrl { get; set; }
        public string UpdatedBy { get; set; }
    }
}