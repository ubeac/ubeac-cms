namespace uBeacCMS.Models;

public class Site : BaseEntity
{
    public required string Name { get; set; }
    public required string Domains { get; set; }
    public required string Title { get; set; }
    public string? Keywords { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string>? Settings { get; set; }
    public required List<Guid> SkinIds { get; set; }
}