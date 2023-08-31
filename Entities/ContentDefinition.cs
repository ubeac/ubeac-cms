﻿namespace Entities;
public class ContentDefinition : IEntity<Guid>
{
    public Guid Id { get; set; }
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