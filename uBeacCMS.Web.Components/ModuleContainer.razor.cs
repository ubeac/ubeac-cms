namespace uBeacCMS.Web.Components;

public partial class ModuleContainer : ComponentBase
{
    [Parameter]
    public Module? Module { get; set; }

    [Parameter]
    public ModuleDefinition? ModuleDefinition { get; set; }

    [Inject]
    public RequestContext? Context { get; set; }

    private RenderFragment RenderModule() => builder =>
    {
        if (ModuleDefinition != null)
        {
            var typeName = "";
            switch (Context?.ViewType)
            {
                case RequestViewType.Edit:
                    typeName = ModuleDefinition?.EditType;
                    break;

                case RequestViewType.Normal:
                    typeName = ModuleDefinition?.ViewType;
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
