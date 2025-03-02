using Microsoft.AspNetCore.Mvc;
using ProductService.Data;
using ProductService.Entities;
using ProductService.EntityDTOs.Category;
using ProductService.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ProductServiceDbContext _context;
        private readonly IMapper _mapper;
        private readonly EntityService _entityService;

        public CategoryController(ProductServiceDbContext context, IMapper mapper, EntityService entityService)
        {
            _context = context;
            _mapper = mapper;
            _entityService = entityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            _entityService.SetCreatedProperties(category, category.CreatedBy);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Manually update each property if it exists in the DTO
            category.Name = !string.IsNullOrEmpty(categoryUpdateDto.Name) ? categoryUpdateDto.Name : category.Name;
            _entityService.SetUpdatedProperties(category, categoryUpdateDto.UpdatedBy);

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}