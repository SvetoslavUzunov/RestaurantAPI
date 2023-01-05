using Restaurant.Services.Identity.Initializer;

namespace Restaurant.Services.Identity;

public static class ConfigureDbInitializer
{
	private static readonly IDbInitializer dbInitializer;

	public static void Execute()
		=> dbInitializer.Initialize();
}
