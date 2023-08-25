namespace uBeacCMS.Web.Components;

public partial class ModuleContainer : ComponentBase
{
    [Parameter]
    public Module? Module { get; set; }

    [Parameter]
    public bool ShowModuleFeatures { get; set; } = false;

    [Parameter]
    public ModuleDefinition? ModuleDefinition { get; set; }

    [Parameter]
    public ViewContext? Context { get; set; }
    
    protected override void OnParametersSet()
    {
        if (ModuleDefinition != null && Context?.ViewType == ViewType.Normal)
            ShowModuleFeatures = true;

        base.OnParametersSet();
    }

    private RenderFragment RenderModule() => builder =>
    {
        if (ModuleDefinition != null)
        {
            var typeName = "";
            switch (Context?.ViewType)
            {
                case ViewType.Edit:
                    typeName = ModuleDefinition?.EditType;
                    break;

                case ViewType.Normal:
                    typeName = ModuleDefinition?.ViewType;
                    //ShowModuleFeatures = true;
                    break;

                default:
                    break;
            }

            // TODO: Find a better way to get type
            // Implement a Service to get type by name and cache
            var moduleType = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.FullName == typeName).SingleOrDefault();
            if (moduleType != null)
            {
                builder.OpenComponent(0, moduleType);
                builder.AddAttribute(0, "Module", Module);
                builder.AddAttribute(1, "ModuleDefinition", ModuleDefinition);
                builder.CloseComponent();
            }
        }
    };

}
