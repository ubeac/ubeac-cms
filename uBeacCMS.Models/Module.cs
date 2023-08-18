namespace uBeacCMS.Models;
public class Module : BaseEntity
{
    public Guid ModuleDefinitionId { get; set; }
    public Guid PageId { get; set; }
    public required string Pane { get; set; }
    public string? Title { get; set; }
    public string? AdministratorRoles { get; set; }
    public string? AuthorizedRoles { get; set; }
    public Dictionary<string, string>? Settings { get; set; }
}