namespace uBeacCMS.Models;

public class Page : BaseEntity
{
    public Guid? ParentId { get; set; }
    public required string Route { get; set; }
    public int Order { get; set; }
    public Guid SiteId { get; set; }
    public string? Title { get; set; }
    public string? Keywords { get; set; }
    public string? Description { get; set; }
    public string? AdministratorRoles { get; set; }
    public string? AuthorizedRoles { get; set; }
    public string? ContributorRoles { get; set; }
    public Dictionary<string, string>? Settings { get; set; }
    public Skin? Skin { get; set; }
}

