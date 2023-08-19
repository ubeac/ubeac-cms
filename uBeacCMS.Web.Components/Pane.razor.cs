using Microsoft.AspNetCore.Components;
using uBeacCMS.Models;

namespace uBeacCMS.Web.Components;

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
            var typeName = moduleDefinition.ViewType;
            
            // TODO: Find a better way to get type
            // Implement a Service to get type by name and cache
            var moduleType = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.FullName == typeName).SingleOrDefault();
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
