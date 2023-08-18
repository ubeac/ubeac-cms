using Microsoft.AspNetCore.Components;
using uBeacCMS.Models;

namespace uBeacCMS.Web.Shared;

public partial class Pane : ComponentBase
{
    [Parameter]
    public string Name { get; set; } = "content";

    [Parameter]
    public List<Module> Modules { get; set; } = new List<Module>();

    [Parameter]
    public List<ModuleDefinition> ModuleDefinitions { get; set; } = new List<ModuleDefinition>();

    private RenderFragment RenderModule(Module module) => builder =>
    {
        var moduleDefinition = GetByModule(module);
        if (moduleDefinition != null)
        {
            var moduleType = Type.GetType(moduleDefinition.ViewType);
            if (moduleType != null)
            {
                builder.OpenComponent(0, moduleType);
                builder.AddAttribute(1, "Module", module);
                builder.AddAttribute(1, "ModuleDefinition", moduleDefinition);
                builder.CloseComponent();
            }
        }
    };

    private ModuleDefinition? GetByModule(Module module) 
    {
        return ModuleDefinitions.Where(x => x.Id == module.ModuleDefinitionId).SingleOrDefault();
    }
}
