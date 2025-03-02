namespace ProductService.EntityDTOs.SubCategory
{
    public class SubCategoryUpdateDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string UpdatedBy { get; set; }
    }
}