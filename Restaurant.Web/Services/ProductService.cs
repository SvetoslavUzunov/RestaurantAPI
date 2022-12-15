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
			ApiType = StaticDetails.ApiType.POST,
			Data = productDto,
			Url = StaticDetails.ProductAPIBase + "/api/products",
			AccessToken = ""
		});

	public async Task<T> DeleteProductAsync<T>(int id)
		=> await this.SendAsync<T>(new ApiRequest()
		{
			ApiType = StaticDetails.ApiType.DELETE,
			Url = StaticDetails.ProductAPIBase + "/api/products/" + id,
			AccessToken = ""
		});

	public async Task<T> GetAllProductsAsync<T>()
		=> await this.SendAsync<T>(new ApiRequest()
		{
			ApiType = StaticDetails.ApiType.GET,
			Url = StaticDetails.ProductAPIBase + "/api/products",
			AccessToken = ""
		});

	public async Task<T> GetProductByIdAsync<T>(int id)
		=> await this.SendAsync<T>(new ApiRequest()
		{
			ApiType = StaticDetails.ApiType.GET,
			Url = StaticDetails.ProductAPIBase + "/api/products/" + id,
			AccessToken = ""
		});

	public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
		=> await this.SendAsync<T>(new ApiRequest()
		{
			ApiType = StaticDetails.ApiType.PUT,
			Data = productDto,
			Url = StaticDetails.ProductAPIBase + "/api/products",
			AccessToken = ""
		});
}
