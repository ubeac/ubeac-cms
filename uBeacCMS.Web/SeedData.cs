using uBeacCMS.Models;
using uBeacCMS.Services;

namespace uBeacCMS.Repositories;

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
                Id = Guid.NewGuid(),
                Name = "Sample Website",
                AdministratorRoles = "Administrators",
                ContributorRoles = "Contributors",
                DefaultRoles = "Registered Users",
                Domains = "localhost:5072",
                Description = "Description",
                Footer = "Default site footer",
                Title = "Title",
                Keywords = "Site keywords",
                Settings = new Dictionary<string, string> { { "test", "amir" } }
            };
            siteService.Add(site);

        }
    }
}
