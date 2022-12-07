using Microsoft.EntityFrameworkCore;
using Restaurant.Services.ProductAPI.Models;

namespace Restaurant.Services.ProductAPI.DbContexts;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	public DbSet<Product> Products { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<Product>().HasData(new Product
		{
			ProductId = 1,
			Name = "Tomato",
			Price = 9,
			Description = "Tomato desc...",
			ImageUrl = "https://dotnettrainingudemy.blob.core.windows.net/restaurant/istock000044051102large.jpg",
			CategoryName = "Veggetable"
		});

		base.OnModelCreating(builder);
	}
}
