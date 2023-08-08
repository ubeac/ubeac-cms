namespace uBeacCMS.Models.Modules;

public class TextHtml : AuditEntity
{
    public string Content { get; set; }
    public Guid ModuleId { get; set; }
}
