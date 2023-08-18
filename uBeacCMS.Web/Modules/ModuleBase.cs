using Microsoft.AspNetCore.Components;
using uBeacCMS.Models;
using uBeacCMS.Services;

namespace uBeacCMS.Web.Modules;

public class ModuleBase : ComponentBase
{
    [Parameter]
    public Module Module { get; set; }
}

public class ModuleBase<T> : ModuleBase where T : IBaseContent
{

    [Inject]
    public IBaseContentService<T> Service { get; set; }

    public T? Content { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var contents = await Service.GetByModuleId(Module.Id);
        Content = contents.SingleOrDefault();
        await base.OnParametersSetAsync();
    }

}

