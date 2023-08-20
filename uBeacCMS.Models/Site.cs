namespace uBeacCMS.Models;

public class Site : BaseEntity
{
    public required string Name { get; set; }
    public required string Domains { get; set; }
    public required string Title { get; set; }
    public string? Keywords { get; set; }
    public string? Description { get; set; }
    // TODO: change this to Guid
    public required string AdministratorRoles { get; set; }
    // TODO: change this to Guid
    public required string DefaultRoles { get; set; }
    // TODO: change this to Guid
    public required string ContributorRoles { get; set; }
    public Dictionary<string, string>? Settings { get; set; }
    // TODO: Extract this to a separate table
    public required List<Skin> Skins { get; set; }
}