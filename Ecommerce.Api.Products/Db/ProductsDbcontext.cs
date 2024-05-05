using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Products.Db;

public class ProductsDbcontext : DbContext
{
	public DbSet<Product> Products { get; set; }
	
	public ProductsDbcontext(DbContextOptions options) : base(options)
	{
	}
}
