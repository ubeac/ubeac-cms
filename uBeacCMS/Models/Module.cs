namespace uBeacCMS.Models;

public class Module : AuditEntity
{
    public string Title { get; set; }
    public Guid ModuleDefinitionId { get; set; }
    public string Template { get; set; }
    public Guid PageId { get; set; }
    public string Pane { get; set; }
    public Dictionary<string,string> Setting { get; set; }
}
