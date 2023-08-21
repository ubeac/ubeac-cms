namespace uBeacCMS.Web.Components;

[RenderModeServer]
public class ModuleBase : ComponentBase
{
    [Parameter]
    public Module Module { get; set; }

    [Parameter]
    public ModuleDefinition ModuleDefinition { get; set; }

}

public class ModuleBase<T> : ModuleBase where T : IBaseContent, new()
{

    [Inject]
    public IBaseContentService<T> Service { get; set; }

    public T Model { get; set; } = new T();

    protected override async Task OnParametersSetAsync()
    {
        var contents = await Service.GetByModuleId(Module.Id);
        Model = contents.Single();
        await base.OnParametersSetAsync();
    }

}

