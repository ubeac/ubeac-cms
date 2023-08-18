namespace uBeacCMS.Models;

public class ModuleDefinition : BaseEntity
{
    public required string Name { get; set; }
    public required string Category { get; set; }
    public string? Description { get; set; }
    public required string ViewType { get; set; }
    public string? EditType { get; set; }
}