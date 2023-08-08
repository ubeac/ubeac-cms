namespace uBeacCMS.Models.Modules;

public class TextHtmlSetting:AuditEntity, ISetting
{
    public int ContentMaxLength { get; set; }
    public Guid ModuleId { get; set; }
}
