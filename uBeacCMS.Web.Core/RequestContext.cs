using uBeacCMS.Models;

namespace uBeacCMS.Web.Core;

public class RequestContext
{
    public Page? Page { get; set; }
    public Site? Site { get; set; }
    public List<Module>? Modules { get; set; }
    public List<ModuleDefinition> ModuleDefinitions { get; set; } = new();
    public RequestViewType ViewType { get; set; } = RequestViewType.Normal;
    public bool IsValid { get; set; } = false;
}

public enum RequestViewType
{
    Normal = 0,
    Edit = 1,
    Setting = 2
}