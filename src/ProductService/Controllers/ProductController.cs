using Microsoft.AspNetCore.Mvc;
using ProductService.Data;
using ProductService.Entities;
using ProductService.EntityDTOs.Product;
using ProductService.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            // Manually update each property using ternary operator
            product.Name = !string.IsNullOrEmpty(productUpdateDto.Name) ? productUpdateDto.Name : product.Name;
            product.Description = !string.IsNullOrEmpty(productUpdateDto.Description) ? productUpdateDto.Description : product.Description;
            product.Cost = productUpdateDto.Cost.HasValue ? productUpdateDto.Cost.Value : product.Cost;
            product.Price = productUpdateDto.Price.HasValue ? productUpdateDto.Price.Value : product.Price;
            product.CategoryId = productUpdateDto.CategoryId.HasValue ? productUpdateDto.CategoryId.Value : product.CategoryId;
            product.ImageUrl = !string.IsNullOrEmpty(productUpdateDto.ImageUrl) ? productUpdateDto.ImageUrl : product.ImageUrl;

            _entityService.SetUpdatedProperties(product, productUpdateDto.UpdatedBy);

            // Update SubCategories
            product.SubCategories.Clear();
            if (productUpdateDto.SubCategoryIds != null && productUpdateDto.SubCategoryIds.Any())
            {
                var subCategories = await _context.SubCategories
                    .Where(sc => productUpdateDto.SubCategoryIds.Contains(sc.Id))
                    .ToListAsync();
                foreach (var subCategory in subCategories)
                {
                    product.SubCategories.Add(subCategory);
                }
            }

            _context.Entry(product).Collection(p => p.SubCategories).IsModified = true;
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