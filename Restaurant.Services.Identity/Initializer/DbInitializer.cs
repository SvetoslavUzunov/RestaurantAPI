using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Restaurant.Services.Identity.DBContexts;
using Restaurant.Services.Identity.Models;

namespace Restaurant.Services.Identity.Initializer;

public class DbInitializer : IDbInitializer
{
	private readonly ApplicationDbContext context;
	private readonly UserManager<ApplicationUser> userManager;
	private readonly RoleManager<IdentityRole> roleManager;

	public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
	{
		this.context = context;
		this.userManager = userManager;
		this.roleManager = roleManager;
	}

	public void Initialize()
	{
		if (roleManager.FindByNameAsync(StaticDetails.Admin).Result is null)
		{
			roleManager.CreateAsync(new IdentityRole(StaticDetails.Admin)).GetAwaiter().GetResult();
			roleManager.CreateAsync(new IdentityRole(StaticDetails.Customer)).GetAwaiter().GetResult();
		}
		else
		{
			return;
		}

		var adminUser = new ApplicationUser
		{
			UserName = "admin1@gmail.com",
			Email = "admin1@gmail.com",
			EmailConfirmed = true,
			PhoneNumber = "11111111",
			FirstName = "Ben",
			LastName = "Admin"
		};

		userManager.CreateAsync(adminUser, "Admin123").GetAwaiter().GetResult();
		userManager.AddToRoleAsync(adminUser, StaticDetails.Admin).GetAwaiter().GetResult();

		var adminClaims = userManager.AddClaimsAsync(adminUser, new Claim[]
		{
			new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
			new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
			new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
			new Claim(JwtClaimTypes.Role, StaticDetails.Admin),
		}).Result;

		var customerUser = new ApplicationUser
		{
			UserName = "customer1@gmail.com",
			Email = "customer1@gmail.com",
			EmailConfirmed = true,
			PhoneNumber = "11111111",
			FirstName = "Ben",
			LastName = "Customer"
		};

		userManager.CreateAsync(customerUser, "Customer123").GetAwaiter().GetResult();
		userManager.AddToRoleAsync(customerUser, StaticDetails.Customer).GetAwaiter().GetResult();

		var customerClaims = userManager.AddClaimsAsync(customerUser, new Claim[]
		{
			new Claim(JwtClaimTypes.Name, customerUser.FirstName +" "+ customerUser.LastName),
			new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
			new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
			new Claim(JwtClaimTypes.Role, StaticDetails.Customer),
		}).Result;
	}
}
