using Restaurant.Web.Models;

namespace Restaurant.Web.Services.IServices;

public interface IProductService : IBaseService
{
	public Task<T> GetAllProductsAsync<T>();

	public Task<T> GetProductByIdAsync<T>(int id);

	public Task<T> CreateProductAsync<T>(ProductDto productDto);

	public Task<T> UpdateProductAsync<T>(ProductDto productDto);

	public Task<T> DeleteProductByIdAsync<T>(int id);
}
