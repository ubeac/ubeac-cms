using uBeacCMS.Repositories.StaticFile;
using uBeacCMS.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class RepositoryExtensions
{
    public static void AddStaticFileRepositories(this IServiceCollection services, Configuration.ConfigurationManager configuration)
    {
        services.Configure<StaticFileRepositorySettings>(configuration.GetSection("FileRepository"));
        services.AddOptions<StaticFileRepositorySettings>();

        services.AddSingleton(typeof(IBaseRepository<>), typeof(StaticFileBaseRepository<>));
        services.AddSingleton(typeof(IBaseContentRepository<>), typeof(StaticFileBaseContentRepository<>));
    }
}
