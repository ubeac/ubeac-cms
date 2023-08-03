using uBeacCMS.Models;
using uBeacCMS.Services;

namespace uBeacCMS;

public static class SeedData
{
    public static void SeedDefaultData(this IServiceProvider provider)
    {
        var scope = provider.CreateScope();

        var siteService = scope.ServiceProvider.GetRequiredService<ISiteService>();
        if (!siteService.GetAll().GetAwaiter().GetResult().Any())
        {
            var site = new Site
            {
                Id = Guid.NewGuid(),
                Name = "Sample Website",
                Administrators = new(),
                Domains = new List<string> { "localhost:5205" },
                Template = ""
            };
            siteService.Create(site).GetAwaiter().GetResult();
                        
            var pageService = scope.ServiceProvider.GetRequiredService<IPageService>();
            var page = new Page
            {
                Id = Guid.NewGuid(),
                Description = "This is home page",
                Keywords = "home",
                SiteId = site.Id,
                Template = "",
                Title = "Home Page",
                Url = "home"
            };
            pageService.Create(page).GetAwaiter().GetResult();

            page = new Page
            {
                Id = Guid.NewGuid(),
                Description = "Contact us",
                Keywords = "contact",
                SiteId = site.Id,
                Template = "",
                Title = "Contact us",
                Url = "contact"
            };
            pageService.Create(page).GetAwaiter().GetResult();

            var moduleDefinitionService = scope.ServiceProvider.GetRequiredService<IModuleDefinitionService>();
            var moduleDefinition = new ModuleDefinition
            {
                Id = Guid.NewGuid(),
                Description = "Simple text viewer",
                Name = "Text viewer",
                Template = ""
            };
            moduleDefinitionService.Create(moduleDefinition).GetAwaiter().GetResult();

            var moduleService = scope.ServiceProvider.GetRequiredService<IModuleService>();
            var module = new Module
            {
                Id = Guid.NewGuid(),
                ModuleDefinitionId= moduleDefinition.Id,
                PageId= page.Id,
                Title = "About us",
                Template = "<h1>About us</h1> <p>This is about us.</p>",
                Pane = ""
            };
            moduleService.Create(module).GetAwaiter().GetResult();
        }        
    }
}
