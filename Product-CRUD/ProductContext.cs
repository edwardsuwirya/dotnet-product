using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCRUD.Model;

namespace Product_CRUD;

public class ProductContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }

    private readonly IConfiguration _configuration;
    public ProductContext(IConfiguration configuration) => _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
}