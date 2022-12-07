using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.ProductAPI.DbContexts;
using Restaurant.Services.ProductAPI.Models;
using Restaurant.Services.ProductAPI.Models.Dto;

namespace Restaurant.Services.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{
	private readonly ApplicationDbContext context;
	private readonly IMapper mapper;

	public ProductRepository(ApplicationDbContext context, IMapper mapper)
	{
		this.context = context;
		this.mapper = mapper;
	}

	public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
	{
		var product = mapper.Map<ProductDto, Product>(productDto);

		if (product.ProductId > 0)
		{
			context.Update(product);
		}
		else
		{
			context.Products.Add(product);
		}

		await context.SaveChangesAsync();

		return mapper.Map<Product, ProductDto>(product);
	}

	public async Task<bool> DeleteProduct(int productId)
	{
		try
		{
			var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
			if (product is null)
			{
				return false;
			}

			context.Remove(product);
			await context.SaveChangesAsync();
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public async Task<ProductDto> GetProductByid(int productId)
	{
		var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

		return mapper.Map<ProductDto>(product);
	}

	public async Task<IEnumerable<ProductDto>> GetProducts()
	{
		var products = await context.Products.ToListAsync();

		return mapper.Map<List<ProductDto>>(products);
	}
}
