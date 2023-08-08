namespace uBeacCMS.Models;

public class ModuleDefinition : AuditEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string, string> ViewTypes { get; set; }
    public Dictionary<string, string> Setting { get; set; }
}
