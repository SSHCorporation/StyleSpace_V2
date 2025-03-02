using Microsoft.EntityFrameworkCore;
using ProductService.Entities;

namespace ProductService.Data
{
    public class ProductServiceDbContext : DbContext
    {
        public ProductServiceDbContext(DbContextOptions<ProductServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                entity.HasMany(e => e.SubCategories)
                      .WithOne(sc => sc.Category)
                      .HasForeignKey(sc => sc.CategoryId);
            });

            // Configure SubCategory entity
            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                entity.HasOne(sc => sc.Category)
                      .WithMany(c => c.SubCategories)
                      .HasForeignKey(sc => sc.CategoryId);
            });

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Cost).HasColumnType("decimal(8,2)");
                entity.Property(e => e.Price).HasColumnType("decimal(8,2)");
                entity.Property(e => e.ImageUrl).HasMaxLength(200);

                entity.HasOne(p => p.Category)
                      .WithMany()
                      .HasForeignKey(p => p.CategoryId);

                entity.HasMany(p => p.SubCategories)
                      .WithMany(sc => sc.Products)
                      .UsingEntity(j => j.ToTable("ProductSubCategories"));
            });
        }
    }
}