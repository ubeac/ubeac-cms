namespace uBeacCMS.Models;

public class Skin : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Markup { get; set; }
    public SkinContainerType ContainerType { get; set; }
    public ViewType Type { get; set; }
}
