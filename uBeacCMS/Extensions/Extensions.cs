using Microsoft.AspNetCore.Mvc;

namespace uBeacCMS;

public static class Extensions
{
    public static IServiceCollection DisableAutomaticModelStateValidation(this IServiceCollection services)
        => services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
}