using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.ProductAPI.Models.Dto;
using Restaurant.Services.ProductAPI.Repository;

namespace Restaurant.Services.ProductAPI.Controllers;

[Route("api/products")]
public class ProductAPIController : ControllerBase
{
	protected readonly ResponseDto response;
	private readonly IProductRepository productRepository;

	public ProductAPIController(IProductRepository productRepository)
	{
		this.productRepository = productRepository;
		this.response = new ResponseDto();
	}

	[HttpGet]
	public async Task<object> GetAll()
	{
		try
		{
			var productsDtos = await productRepository.GetProducts();
			response.Result = productsDtos;
		}
		catch (Exception ex)
		{
			response.IsSuccess = false;
			response.ErrorMessages = new List<string> { ex.Message };
		}

		return response;
	}

	[HttpGet]
	[Route("{id}")]
	public async Task<object> GetById(int id)
	{
		try
		{
			var product = await productRepository.GetProductByid(id);

			response.Result = product;
		}
		catch (Exception ex)
		{
			response.IsSuccess = false;
			response.ErrorMessages = new List<string> { ex.Message };
		}

		return response;
	}
}
