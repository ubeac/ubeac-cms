using Microsoft.Extensions.Options;
using uBeacCMS.Models;

namespace uBeacCMS.Repositories.StaticFile;

public class StaticFileBaseContentRepository<T> : StaticFileBaseRepository<T>, IBaseContentRepository<T> where T : IBaseContent
{
    public StaticFileBaseContentRepository(IOptions<StaticFileRepositorySettings> options) : base(options)
    {
    }

    public virtual async Task<IList<T>> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default)
    {
        var values = await GetAll(cancellationToken).ConfigureAwait(false);
        return values.Where(x => x.ModuleId == moduleId).ToList();
    }
}

