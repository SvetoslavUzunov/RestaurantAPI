using Microsoft.EntityFrameworkCore;
using Restaurant.Services.ProductAPI;
using Restaurant.Services.ProductAPI.DbContexts;
using Restaurant.Services.ProductAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var connection = "Server=.\\SQLEXPRESS;Database=RestaurantAPI;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(connection);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
