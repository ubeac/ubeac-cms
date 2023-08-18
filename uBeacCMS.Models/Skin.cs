namespace uBeacCMS.Models;

public class Skin
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Markup { get; set; }
    public SkinType Type { get; set; }
    public Guid? ContainerId { get; set; }
}
