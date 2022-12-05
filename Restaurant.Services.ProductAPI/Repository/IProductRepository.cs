using Restaurant.Services.ProductAPI.Models.Dto;

namespace Restaurant.Services.ProductAPI.Repository;

public interface IProductRepository
{
	public Task<IEnumerable<ProductDto>> GetProducts();

	public Task<ProductDto> GetProductByid(int productId);

	public Task<ProductDto> CreateProduct(ProductDto productDto);

	public Task<bool> DeleteProduct(int productId);
}
