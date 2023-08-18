using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IBaseContentService<T> : IBaseService<T> where T : IBaseContent
{
    Task<IList<T>> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default);
}

public class BaseContentService<T> : BaseService<T>, IBaseContentService<T> where T : IBaseContent
{
    protected readonly IBaseContentRepository<T> BaseContentRepository;

    public BaseContentService(IBaseContentRepository<T> baseContentRepository) : base(baseContentRepository)
    {
        BaseContentRepository = baseContentRepository;
    }

    public Task<IList<T>> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default)
    {
        return BaseContentRepository.GetByModuleId(moduleId, cancellationToken);
    }
}
