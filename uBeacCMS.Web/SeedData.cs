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
                         Markup=@"<!doctype html>
<html lang=""en"">
<head>
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <base href=""/"" />
    <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css"" rel=""stylesheet"" integrity=""sha384-4bw+/aepP/YC94hEpVNVgiZdgIC5+VKNBQNGCHeKRQN+PtmoHDEXuppvnDJzQIu9"" crossorigin=""anonymous"">
    <title>Bootstrap demo</title>
</head>
<body>
    <div class=""container text-center"">
        <div class=""row"">
            <div class=""col border"">
                <pane name=""top""></pane>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-2 border"">
                <pane name=""left""></pane>
            </div>
            <div class=""col border"">
                <pane name=""content""></pane>
            </div>
            <div class=""col-2 border"">
                <pane name=""right""></pane>
            </div>
        </div>
    </div>
    <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/js/bootstrap.bundle.min.js"" integrity=""sha384-HwwvtgBNo3bZJJLYd8oVXjrBZt8cqVSpeBNS5n7C8IVInixGAoxmnlMuBnhbgrkm"" crossorigin=""anonymous""></script>
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
                    ViewType = "uBeacCMS.Web.Modules.TextHtml.View",
                    Name = "Text/Html",
                    Category = "General",
                    EditType = "uBeacCMS.Web.Modules.TextHtml.Edit"
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
                    Value = "TextHtmlContent 1",
                    ModuleId = modules[0].Id
                },
                new TextHtmlContent
                {
                    Id= Guid.NewGuid(),
                    Value = "TextHtmlContent 2",
                    ModuleId = modules[1].Id
                },
                new TextHtmlContent
                {
                    Id= Guid.NewGuid(),
                    Value = "TextHtmlContent 3",
                    ModuleId = modules[2].Id
                }
            };

            textHtmlService.AddRange(textHtmlContents).GetAwaiter();

        }
    }
}
