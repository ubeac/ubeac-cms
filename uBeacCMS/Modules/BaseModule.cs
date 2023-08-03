using Microsoft.AspNetCore.Components;
using uBeacCMS.Models;

namespace uBeacCMS.Modules;

public class BaseModule : ComponentBase
{
    [Parameter]
    public CmsContext Core { get; set; }

    [Parameter]
    public Module Module { get; set; }
}
