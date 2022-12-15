using Newtonsoft.Json;
using Restaurant.Web.Models;
using Microsoft.AspNetCore.Mvc;
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

	public IActionResult ProductCreate()
		=> View();

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ProductCreate(ProductDto model)
	{
		if (ModelState.IsValid)
		{
			var response = await productService.CreateProductAsync<ResponseDto>(model);
			if (response is not null && response.IsSuccess)
			{
				return RedirectToAction(nameof(ProductIndex));
			}
		}

		return View(model);
	}

	public async Task<IActionResult> ProductEdit(int productId)
	{
		var response = await productService.GetProductByIdAsync<ResponseDto>(productId);
		if (response is not null && response.IsSuccess)
		{
			var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
			return View(model);
		}

		return NotFound();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ProductEdit(ProductDto model)
	{
		if (ModelState.IsValid)
		{
			var response = await productService.UpdateProductAsync<ResponseDto>(model);
			if (response is not null && response.IsSuccess)
			{
				return RedirectToAction(nameof(ProductIndex));
			}
		}

		return View(model);
	}

	public async Task<IActionResult> ProductDelete(int productId)
	{
		var response = await productService.GetProductByIdAsync<ResponseDto>(productId);

		if (response is not null && response.IsSuccess)
		{
			var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
			return View(model);
		}

		return NotFound();
	}

	[HttpDelete]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ProductDelete(ProductDto model)
	{
		if (ModelState.IsValid)
		{
			var response = await productService.DeleteProductAsync<ResponseDto>(model.ProductId);
			if(response.IsSuccess)
			{
				return RedirectToAction(nameof(ProductIndex));
			}
		}

		return View(model);
	}
}
