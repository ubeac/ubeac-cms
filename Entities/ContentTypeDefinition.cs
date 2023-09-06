namespace Entities;

public class ContentTypeDefinition : BaseEntity
{
    public Guid SiteId { get; set; }
    public string Name { get; set; }
    public List<FieldDefinition> Fields { get; set; }
}

public class FieldDefinition
{
    public string Label { get; set; }
    public string DefaultValue { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
}