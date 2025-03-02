using Microsoft.AspNetCore.Mvc;
using ProductService.Data;
using ProductService.Entities;
using ProductService.EntityDTOs.SubCategory;
using ProductService.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ProductServiceDbContext _context;
        private readonly IMapper _mapper;
        private readonly EntityService _entityService;

        public SubCategoryController(ProductServiceDbContext context, IMapper mapper, EntityService entityService)
        {
            _context = context;
            _mapper = mapper;
            _entityService = entityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryDto>>> GetSubCategories()
        {
            var subCategories = await _context.SubCategories.ToListAsync();
            var subCategoryDtos = _mapper.Map<List<SubCategoryDto>>(subCategories);

            return Ok(subCategoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryDto>> GetSubCategory(Guid id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);

            if (subCategory == null)
            {
                return NotFound();
            }

            var subCategoryDto = _mapper.Map<SubCategoryDto>(subCategory);
            return Ok(subCategoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<SubCategoryDto>> CreateSubCategory(SubCategoryCreateDto subCategoryCreateDto)
        {
            var subCategory = _mapper.Map<SubCategory>(subCategoryCreateDto);
            _entityService.SetCreatedProperties(subCategory, subCategory.CreatedBy);

            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();

            var subCategoryDto = _mapper.Map<SubCategoryDto>(subCategory);

            return CreatedAtAction(nameof(GetSubCategory), new { id = subCategory.Id }, subCategoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(Guid id, SubCategoryUpdateDto subCategoryUpdateDto)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            // Manually update each property if it exists in the DTO
            subCategory.Name = !string.IsNullOrEmpty(subCategoryUpdateDto.Name) ? subCategoryUpdateDto.Name : subCategory.Name;
            subCategory.CategoryId = subCategoryUpdateDto.CategoryId.HasValue ? subCategoryUpdateDto.CategoryId.Value : subCategory.CategoryId;

            _entityService.SetUpdatedProperties(subCategory, subCategoryUpdateDto.UpdatedBy);

            _context.Entry(subCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(Guid id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}