namespace uBeacCMS.Models;

public class Site:AuditEntity
{
    public string Name { get; set; }
    public List<string> Domains { get; set; }
    public List<Guid> Administrators { get; set; }
    public string Template { get; set; }
}
