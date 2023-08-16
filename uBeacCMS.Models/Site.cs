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
}

public class Page : BaseEntity
{
    public Guid? ParentId { get; set; }
    public string Route { get; set; }
    public int Order { get; set; }
    public Guid SiteId { get; set; }
    public string? Title { get; set; }
    public string? Keywords { get; set; }
    public string? Description { get; set; }
    public string? AdministratorRoles { get; set; }
    public string? AuthorizedRoles { get; set; }
    public string? ContributorRoles { get; set; }
    public Dictionary<string, string>? Settings { get; set; }
}

public class ModuleDefinition : BaseEntity
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
}

public class Module : BaseEntity
{
    public Guid ModuleDefinitionId { get; set; }
    public ModuleDefinition ModuleDefinition { get; set; }
    public Guid PageId { get; set; }
    public Page Page { get; set; }
    public string Pane { get; set; }
    public string Title { get; set; }
    public string AdministratorRoles { get; set; }
    public string AuthorizedRoles { get; set; }
    public List<BaseEntitySetting> Settings { get; set; }
}

public class BaseEntitySetting
{
    public Guid EntityId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}