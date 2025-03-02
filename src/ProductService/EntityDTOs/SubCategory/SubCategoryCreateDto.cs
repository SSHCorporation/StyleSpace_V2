namespace ProductService.EntityDTOs.SubCategory
{
    public class SubCategoryCreateDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string CreatedBy { get; set; }
    }
}