using uBeacCMS.Models;
using uBeacCMS.Services;
using uBeacCMS.Web.Modules.TextHtml;

namespace uBeacCMS.Repositories;

public static class SeedData
{
    public static void SeedDefaultData(this IServiceProvider provider)
    {
        var scope = provider.CreateScope();

        var siteService = scope.ServiceProvider.GetRequiredService<ISiteService>();
        var pageService = scope.ServiceProvider.GetRequiredService<IPageService>();
        var moduleDefinitionService = scope.ServiceProvider.GetRequiredService<IModuleDefinitionService>();
        var moduleService = scope.ServiceProvider.GetRequiredService<IModuleService>();
        var textHtmlService = scope.ServiceProvider.GetRequiredService<IBaseContentService<TextHtmlContent>>();

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
                Settings = new Dictionary<string, string> { { "test", "amir" } },
                Skins = new List<Skin>
                {
                    new Skin
                    {
                         Name = "Default",
                         Type= SkinType.Site,
                         Markup=@"
<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"" />
    <base href=""/"" />
    <link href=""https://cdnjs.cloudflare.com/ajax/libs/flowbite/1.8.0/flowbite.min.css"" rel=""stylesheet"" />
    <link rel=""stylesheet"" href=""css/app.css"" />
    <link rel=""icon"" type=""image/png"" href=""favicon.png"" />
    <link rel=""stylesheet"" href=""uBeacCMS.Web.styles.css"" />
</head>

<body>
    <div class=""antialiased bg-gray-50 dark:bg-gray-900"">
        <main class=""p-4 md:ml-64 h-auto"">
            <div class=""border-2 border-dashed rounded-lg border-gray-300 dark:border-gray-600 mb-4"">
                <pane name=""top""></pane>
            </div>
            <div class=""grid grid-cols-3 gap-4 mb-4"">
                <div class=""border-2 border-dashed rounded-lg border-gray-300 dark:border-gray-600"">
                    <pane name=""left""></pane>
                </div>
                <div class=""border-2 border-dashed rounded-lg border-gray-300 dark:border-gray-600"">
                    <pane name=""content""></pane>
                </div>
                <div class=""border-2 border-dashed rounded-lg border-gray-300 dark:border-gray-600"">
                    <pane name=""right""></pane>
                </div>
            </div>
        </main>
    </div>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/flowbite/1.8.0/flowbite.min.js""></script>
    <script src=""_framework/blazor.web.js"" suppress-error=""BL9992""></script>
</body>
</html>
"
                    }
                }
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

            var moduleDefinitions = new List<ModuleDefinition>
            {
                new ModuleDefinition
                {
                    Id = Guid.NewGuid(),
                    ViewType = "TextHtmlView",
                    Name = "Text/Html",
                    Category = "General",
                    EditType = "TextHtmlEdit"
                }
            };

            moduleDefinitionService.AddRange(moduleDefinitions).GetAwaiter();

            var modules = new List<Module>
            {
                new Module
                {
                    Id = Guid.NewGuid(),
                    ModuleDefinitionId = moduleDefinitions[0].Id,
                    PageId = pages[0].Id,
                    Pane = "content",
                    Title = "Hello text html 1"
                },
                new Module
                {
                    Id = Guid.NewGuid(),
                    ModuleDefinitionId = moduleDefinitions[0].Id,
                    PageId = pages[0].Id,
                    Pane = "content",
                    Title = "Hello text html 2"
                },
                new Module
                {
                    Id = Guid.NewGuid(),
                    ModuleDefinitionId = moduleDefinitions[0].Id,
                    PageId = pages[0].Id,
                    Pane = "left",
                    Title = "Hello text html 3"
                }
            };

            moduleService.AddRange(modules).GetAwaiter();


            var textHtmlContents = new List<TextHtmlContent> 
            {
                new TextHtmlContent
                {
                    Id= Guid.NewGuid(),
                    Content = "TextHtmlContent 1",
                    ModuleId = modules[0].Id
                },
                new TextHtmlContent
                {
                    Id= Guid.NewGuid(),
                    Content = "TextHtmlContent 2",
                    ModuleId = modules[1].Id
                },
                new TextHtmlContent
                {
                    Id= Guid.NewGuid(),
                    Content = "TextHtmlContent 3",
                    ModuleId = modules[2].Id
                }
            };

            textHtmlService.AddRange(textHtmlContents).GetAwaiter();

        }
    }
}
