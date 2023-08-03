﻿namespace uBeacCMS.Models;

public class CmsContext
{
    public Site Site { get; set; }
    public Page Page { get; set; }
    public List<Module> Modules { get; set; } = new();
    public List<ModuleDefinition> ModuleDefinitions { get; set; } = new();
 }
