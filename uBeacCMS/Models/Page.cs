namespace uBeacCMS.Models;

public class Page : AuditEntity
{
    public string Title { get; set; }
    public string Url { get; set; }
    public string Keywords { get; set; }
    public string Description { get; set; }
    public Guid SiteId { get; set; }
    public string Template { get; set; }
    public bool IsSystem { get; set; } = false;
}
