using AutoMapper;
using Restaurant.Services.ProductAPI.Models;
using Restaurant.Services.ProductAPI.Models.Dto;

namespace Restaurant.Services.ProductAPI;

public class MappingConfig
{
	public static MapperConfiguration RegisterMaps()
	=> new(config =>
		{
			config.CreateMap<ProductDto, Product>();
			config.CreateMap<Product, ProductDto>();
		});
}
