namespace uBeacCMS.Models.Modules;

public class Image:AuditEntity
{
    public string Src { get; set; }
    public string Alt { get; set; }
    public Guid ModuleId { get; set; }
}
