using Microsoft.AspNetCore.Mvc;
using ProductService.Data;
using ProductService.Entities;
using ProductService.EntityDTOs.Product;
using ProductService.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductServiceDbContext _context;
        private readonly IMapper _mapper;
        private readonly EntityService _entityService;

        public ProductController(ProductServiceDbContext context, IMapper mapper, EntityService entityService)
        {
            _context = context;
            _mapper = mapper;
            _entityService = entityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.SubCategories)
                .ToListAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.SubCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            product.Id = Guid.NewGuid();
            _entityService.SetCreatedProperties(product, product.CreatedBy);

            // Add SubCategories
            var subCategories = await _context.SubCategories
                .Where(sc => productCreateDto.SubCategoryIds.Contains(sc.Id))
                .ToListAsync();
            foreach (var subCategory in subCategories)
            {
                product.SubCategories.Add(subCategory);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductUpdateDto productUpdateDto)
        {
            var product = await _context.Products
                .Include(p => p.SubCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _mapper.Map(productUpdateDto, product);
            _entityService.SetUpdatedProperties(product, product.UpdatedBy);

            // Update SubCategories
            product.SubCategories.Clear();
            var subCategories = await _context.SubCategories
                .Where(sc => productUpdateDto.SubCategoryIds.Contains(sc.Id))
                .ToListAsync();
            foreach (var subCategory in subCategories)
            {
                product.SubCategories.Add(subCategory);
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}