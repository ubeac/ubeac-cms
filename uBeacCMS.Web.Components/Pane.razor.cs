namespace uBeacCMS.Web.Components;

public partial class Pane : ComponentBase
{
    [Parameter]
    public string Name { get; set; } = "default";

    [Parameter]
    public List<Module> Modules { get; set; } = new List<Module>();

    [Parameter]
    public List<ModuleDefinition> ModuleDefinitions { get; set; } = new List<ModuleDefinition>();
    
    private ModuleDefinition? GetByModule(Module module) 
    {
        return ModuleDefinitions.Where(x => x.Id == module.ModuleDefinitionId).SingleOrDefault();
    }
}
