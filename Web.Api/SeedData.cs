using Entities;
using Services;

public static class SeedData
{
    public static void SeedDefaultData(this IServiceProvider provider)
    {
        var scope = provider.CreateScope();

        var siteService = scope.ServiceProvider.GetRequiredService<ISiteService>();
        if (!siteService.GetAll().GetAwaiter().GetResult().Any())
        {
            // Create Site
            var site = new Site
            {
                Name = "Sample Website",
                Domain = "localhost:7101"
            };

            siteService.Insert(site).GetAwaiter().GetResult();
        }
    }
}
