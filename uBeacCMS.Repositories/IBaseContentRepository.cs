using uBeacCMS.Models;

namespace uBeacCMS.Repositories;

public interface IBaseContentRepository<T> : IBaseRepository<T> where T : IBaseContent
{
    Task<IList<T>> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default);
}