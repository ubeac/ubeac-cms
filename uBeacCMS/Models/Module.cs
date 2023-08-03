namespace uBeacCMS.Models;

public class Module : AuditEntity
{
    public string Title { get; set; }
    public Guid ModuleDefinitionId { get; set; }
    public string Template { get; set; }
}
