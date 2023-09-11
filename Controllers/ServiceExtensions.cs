using Controllers;
using Entities;
using Microsoft.AspNetCore.Identity;
using Services;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IMvcBuilder AddCmsControllers(this IMvcBuilder mvcBuilder)
    {
        //var assembly = Assembly.GetAssembly(typeof(BaseContentController<>));
        //mvcBuilder.AddApplicationPart(assembly).AddControllersAsServices();
        return mvcBuilder;
    }

    public static IdentityBuilder AddIdentityUser<TUser>(this IServiceCollection services) where TUser : User
    {
        services.AddDataProtection();

        IdentityBuilder identityBuilder = services.AddIdentityCore<TUser>();
        identityBuilder.AddUserStore<UserStore<TUser>>().AddUserManager<UserManager<TUser>>().AddDefaultTokenProviders();

        return identityBuilder;
    }
}
