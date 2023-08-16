using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IBaseService<T> where T : IBaseEntity
{
    Task<IList<T>> GetAll(CancellationToken cancellationToken = default);
    Task<T> Add(T entity, CancellationToken cancellationToken = default);
    Task<T> Update(T entity, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
    Task<T> GetById(Guid id, CancellationToken cancellationToken = default);
}

public class BaseService<T> : IBaseService<T> where T : IBaseEntity
{
    protected readonly IBaseRepository<T> BaseRepository;

    public BaseService(IBaseRepository<T> baseRepository)
    {
        BaseRepository = baseRepository;
    }
    public Task<T> Add(T entity, CancellationToken cancellationToken = default)
    {
        return BaseRepository.Add(entity, cancellationToken);
    }

    public Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        return BaseRepository.Delete(id, cancellationToken);
    }

    public Task<IList<T>> GetAll(CancellationToken cancellationToken = default)
    {
        return BaseRepository.GetAll(cancellationToken);
    }

    public Task<T> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return BaseRepository.GetById(id, cancellationToken);
    }

    public Task<T> Update(T entity, CancellationToken cancellationToken = default)
    {
        return BaseRepository.Update(entity, cancellationToken);
    }
}
