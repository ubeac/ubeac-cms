using uBeacCMS.Models;
using uBeacCMS.Services;

namespace uBeacCMS.Repositories;

public static class SeedData
{
    public static void SeedDefaultData(this IServiceProvider provider)
    {
        var scope = provider.CreateScope();

        var siteService = scope.ServiceProvider.GetRequiredService<ISiteService>();
        var pageService = scope.ServiceProvider.GetRequiredService<IPageService>();

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
                Title = "Title",
                Keywords = "Site keywords",
                Settings = new Dictionary<string, string> { { "test", "amir" } }
            };

            siteService.Add(site).GetAwaiter();

            var pages = new List<Page>
            { 
                new Page
                {
                    Id = Guid.NewGuid(),
                    Order = 1,
                    SiteId = site.Id,
                    Title = "Home",
                    Route = "/"
                },
                new Page
                {
                    Id = Guid.NewGuid(),
                    Order = 3,
                    SiteId = site.Id,
                    Title = "About",
                    Route = "/about"
                },
                new Page
                {
                    Id = Guid.NewGuid(),
                    Order = 2,
                    SiteId = site.Id,
                    Title = "Servers",
                    Route = "servers"
                },
            };

            pages.Add(new Page 
            {
                Id = Guid.NewGuid(),
                Order = 1,
                SiteId = site.Id,
                Title = "HP Servers",
                Route = "hp",
                ParentId = pages[2].Id
            });

            pages.Add(new Page
            {
                Id = Guid.NewGuid(),
                Order = 2,
                SiteId = site.Id,
                Title = "DELL Servers",
                Route = "dell",
                ParentId = pages[2].Id
            });

            pageService.AddRange(pages).GetAwaiter();

        }
    }
}
