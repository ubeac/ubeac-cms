using uBeacCMS.Models;

namespace uBeacCMS.Web.Core;

public class ViewContext
{
    public Page? Page { get; set; }
    public Site? Site { get; set; }
    public List<Skin>? SiteSkins { get; set; }
    public Skin? Skin { get; set; }
    public List<Module>? Modules { get; set; }
    public List<ModuleDefinition> ModuleDefinitions { get; set; } = new();
    public ViewType ViewType { get; set; } = ViewType.Normal;
}

