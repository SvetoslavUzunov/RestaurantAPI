using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.Identity.DBContexts;	
using Restaurant.Services.Identity.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connection = "Server=.\\SQLEXPRESS;Database=RestaurantAPI;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(connection);
});

builder.Services
	.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
