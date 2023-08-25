namespace uBeacCMS.Web.Components;

public partial class Pane : ComponentBase
{
    [Parameter]
    public string Name { get; set; } = "default";

    [Parameter]
    public List<Module> Modules { get; set; } = new List<Module>();

    [Parameter]
    public ViewContext? Context { get; set; }

    private ModuleDefinition? GetByModule(Module module)
    {
        return Context?.ModuleDefinitions.Where(x => x.Id == module.ModuleDefinitionId).SingleOrDefault();
    }
}
