using Restaurant.Services.ProductAPI.Models.Dto;
using Restaurant.Web.Models;
using Restaurant.Web.Services.IServices;

namespace Restaurant.Web.Services;

public class ProductService : BaseService, IProductService
{
	private readonly IHttpClientFactory clientFactory;

	public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
	{
		this.clientFactory = clientFactory;
	}

	public async Task<T> CreateProductAsync<T>(ProductDto productDto)
		=> await this.SendAsync<T>(new ApiRequest()
		{
			ApiType = StaticData.ApiType.POST,
			Data = productDto,
			Url = StaticData.ProductAPIBase + "/api/products",
			AccessToken = ""
		});

	public Task<T> DeleteProductByIdAsync<T>(int id)
	{
		return this.SendAsync<T>(new ApiRequest()
		{
			ApiType = StaticData.ApiType.POST,
			Data = productDto,
			Url = StaticData.ProductAPIBase + "/api/products",
			AccessToken = ""
		});
	}

	public Task<T> GetAllProductsAsync<T>()
	{
		throw new NotImplementedException();
	}

	public Task<T> GetProductByIdAsync<T>(int id)
	{
		throw new NotImplementedException();
	}

	public Task<T> UpdateProductAsync<T>(ProductDto productDto)
	{
		throw new NotImplementedException();
	}
}
