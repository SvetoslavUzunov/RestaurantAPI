using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.Web.Models;
using Restaurant.Web.Services.IServices;

namespace Restaurant.Web.Controllers;

public class ProductController : Controller
{
	private readonly IProductService productService;

	public ProductController(IProductService productService)
	{
		this.productService = productService;
	}

	public async Task<IActionResult> ProductIndex()
	{
		var productsList = new List<ProductDto>();
		var response = await productService.GetAllProductsAsync<ResponseDto>();

		if (response is not null && response.IsSuccess)
		{
			productsList = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
		}

		return View(productsList);
	}
}
