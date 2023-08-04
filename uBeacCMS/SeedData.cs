using uBeacCMS.Models;
using uBeacCMS.Models.Modules;
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
            // Create Site
            var site = new Site
            {
                Id = Guid.NewGuid(),
                Name = "Sample Website",
                Administrators = new(),
                Domains = new List<string> { "localhost:5205" },
                Template = @"
@using uBeacCMS.Models
@using uBeacCMS.Pages.Shared
@using uBeacCMS.Pages
@inject CmsContext Core

<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>@ViewData[""Title""] - uBeacCMS</title>
    <link rel=""stylesheet"" href=""~/lib/bootstrap/dist/css/bootstrap.min.css"" />
    <link rel=""stylesheet"" href=""~/css/site.css"" asp-append-version=""true"" />
    <link rel=""stylesheet"" href=""~/uBeacCMS.styles.css"" asp-append-version=""true"" />
</head>
<body>
    <div class=""container"">
        <header class=""row"">
            <div class=""col border"">
            Header
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Header""' param-core=""@Core"" />
            </div>
        </header>
        <main role=""main"" class=""row"">
            <div class=""col-2 border"">
            Sidebar
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Sidebar""' param-core=""@Core"" />
            </div>
            <div class=""col-8  border"">
            Content
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Content""' param-core=""@Core"" />
                @RenderBody()
            </div>
            <div class=""col-2  border"">
            Right
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Right""' param-core=""@Core"" />
            </div>
        </main>
        <footer class=""row"">
            <div class=""col border"">
            Footer
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Footer""' param-core=""@Core"" />
            </div>
        </footer>
    </div>

    <script src=""~/lib/jquery/dist/jquery.min.js""></script>
    <script src=""~/lib/bootstrap/dist/js/bootstrap.bundle.min.js""></script>
    <script src=""~/js/site.js"" asp-append-version=""true""></script>
</body>
</html>
"
            };
            siteService.Create(site).GetAwaiter().GetResult();

            // Create Page
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
                Title = "Contact us",
                Url = "contact",
                Template = @"
@using uBeacCMS.Models
@using uBeacCMS.Pages.Shared
@using uBeacCMS.Pages
@inject CmsContext Core

<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>@ViewData[""Title""] - uBeacCMS</title>
    <link rel=""stylesheet"" href=""~/lib/bootstrap/dist/css/bootstrap.min.css"" />
    <link rel=""stylesheet"" href=""~/css/site.css"" asp-append-version=""true"" />
    <link rel=""stylesheet"" href=""~/uBeacCMS.styles.css"" asp-append-version=""true"" />
</head>
<body>
    <div class=""container"">
        <header class=""row"">
            <div class=""col border"">
                This is custom template
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Header""' param-core=""@Core"" />
            </div>
        </header>
        <main role=""main"" class=""row"">
            <div class=""col-12  border"">
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Content""' param-core=""@Core"" />
                @RenderBody()
            </div>
        </main>
        <footer class=""row"">
            <div class=""col border"">
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Footer""' param-core=""@Core"" />
            </div>
        </footer>
    </div>

    <script src=""~/lib/jquery/dist/jquery.min.js""></script>
    <script src=""~/lib/bootstrap/dist/js/bootstrap.bundle.min.js""></script>
    <script src=""~/js/site.js"" asp-append-version=""true""></script>
</body>
</html>
",
            };
            pageService.Create(page).GetAwaiter().GetResult();

            // Create ModuleDefinition
            var moduleDefinitionService = scope.ServiceProvider.GetRequiredService<IModuleDefinitionService>();
            var moduleDefinition = new ModuleDefinition
            {
                Id = Guid.NewGuid(),
                Description = "Simple TextHTML",
                Name = "TextHtml",
                Template = ""
            };
            moduleDefinitionService.Create(moduleDefinition).GetAwaiter().GetResult();

            var imageModule = new ModuleDefinition
            {
                Id = Guid.NewGuid(),
                Description = "Simple Image with src and alt props",
                Name = "Image",
                Template = "<img src=\"src\" alt=\"alt\"/>"
            };
            moduleDefinitionService.Create(imageModule).GetAwaiter().GetResult();

            // Create Module
            var moduleService = scope.ServiceProvider.GetRequiredService<IModuleService>();
            var module = new Module
            {
                Id = Guid.NewGuid(),
                ModuleDefinitionId = moduleDefinition.Id,
                PageId = page.Id,
                Title = "About us",
                Template = "Content",
                Pane = ""
            };
            moduleService.Create(module).GetAwaiter().GetResult();

            module = new Module
            {
                Id = Guid.NewGuid(),
                ModuleDefinitionId = moduleDefinition.Id,
                PageId = page.Id,
                Title = "About us",
                Template = "",
                Pane = "Content"
            };
            moduleService.Create(module).GetAwaiter().GetResult();

            module = new Module
            {
                Id = Guid.NewGuid(),
                ModuleDefinitionId = imageModule.Id,
                PageId = page.Id,
                Title = "Image Module Title",
                Template = "",
                Pane = "Content"
            };
            moduleService.Create(module).GetAwaiter().GetResult();


// var textHtmlService = scope.ServiceProvider.GetRequiredService<ITextHtmlService>();
//             var textHtml = new TextHtml
//             {
//                 Id = Guid.NewGuid(),
//                 Content = "<h1>About us</h1> <p>This is about us.</p>",
//                 ModuleId = module.Id
//             };
//             textHtmlService.Create(textHtml).GetAwaiter().GetResult();
            // Create TextHtml
            var textHtmlService = scope.ServiceProvider.GetRequiredService<ITextHtmlService>();
            var textHtml = new TextHtml
            {
                Id = Guid.NewGuid(),
                Content = "<h1>About us</h1> <p>This is about us.</p>",
                ModuleId = module.Id
            };
            textHtmlService.Create(textHtml).GetAwaiter().GetResult();
        }
    }
}
