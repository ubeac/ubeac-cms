namespace uBeacCMS.Models;

public class ModuleDefinition : AuditEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Template { get; set; }
}
