namespace Entities;

public class ContentType : BaseEntity
{
    public Guid SiteId { get; set; }
    public string Name { get; set; }
    public List<Field> Fields { get; set; }
}

public class Field
{
    public string Label { get; set; }
    public string DefaultValue { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
}