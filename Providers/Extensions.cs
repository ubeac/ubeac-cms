using Microsoft.Extensions.Configuration;
using Providers;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddEmailProvider(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<EmailProviderOptions>(config.GetSection("Email"));
        services.AddScoped<IEmailProvider, EmailProvider>();
        return services;
    }
}