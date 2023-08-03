﻿using uBeacCMS.Models;
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
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Header""' param-core=""@Core"" />
            </div>
        </header>
        <main role=""main"" class=""row"">
            <div class=""col-2 border"">
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Sidabar""' param-core=""@Core"" />
            </div>
            <div class=""col-8  border"">
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Content""' param-core=""@Core"" />
            </div>
            <div class=""col-2  border"">
                <component type=""typeof(Pane)"" render-mode=""Static"" param-name='""Right""' param-core=""@Core"" />
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