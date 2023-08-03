using Microsoft.AspNetCore.Components;
using uBeacCMS.Models;

namespace uBeacCMS.Pages;

public partial class Pane : ComponentBase
{
    [Parameter]
    public CmsContext Core { get; set; }

    [Parameter]
    public string Name { get; set; }

    public RenderFragment RenderModules() => builder =>
    {
        if (Core.Modules.Any())
        {
            int i = 0;
            foreach (var module in Core.Modules.Where(x => x.Pane.ToLower() == Name.ToLower()))
            {
                var typeName = Core.ModuleDefinitions.SingleOrDefault(x => x.Id == module.ModuleDefinitionId)?.Name;
                var type = Type.GetType($"uBeacCMS.Modules.{typeName}");

                if (type is null)
                    throw new Exception("Module not found!");
                
                builder.OpenComponent(++i, type);
                builder.AddAttribute(++i, "core", Core);
                builder.AddAttribute(++i, "module", module);
                builder.CloseComponent();
            }
        }
    };

}