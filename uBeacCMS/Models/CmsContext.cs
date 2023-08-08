namespace uBeacCMS.Models;

public class CmsContext
{
    public Site Site { get; set; }
    public Page Page { get; set; }
    public List<Module> Modules { get; set; } = new();
    public Module Module { get; set; }
    public string ModuleAction { get; set; }
    public List<ModuleDefinition> ModuleDefinitions { get; set; } = new();
    public ModuleDefinition ModuleDefinition { get; set; } 
}
