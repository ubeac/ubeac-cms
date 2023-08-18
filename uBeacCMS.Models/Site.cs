namespace uBeacCMS.Models;

public class Site : BaseEntity
{
    public required string Name { get; set; }
    public required string Domains { get; set; }
    public required string Title { get; set; }
    public string? Keywords { get; set; }
    public string? Description { get; set; }
    public required string AdministratorRoles { get; set; }
    public required string DefaultRoles { get; set; }
    public required string ContributorRoles { get; set; }
    public Dictionary<string, string>? Settings { get; set; }
    public required List<Skin> Skins { get; set; }
}