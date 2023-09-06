using Controllers;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IMvcBuilder AddCmsControllers(this IMvcBuilder mvcBuilder)
    {
        var assembly = Assembly.GetAssembly(typeof(BaseContentController<>));
        mvcBuilder.AddApplicationPart(assembly).AddControllersAsServices();
        return mvcBuilder;
    }
}
