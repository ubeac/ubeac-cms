using Entities;
using Repositories;

namespace Services;

public interface IContentService
{
    Task<List<Content>> GetAll(string type, CancellationToken cancellationToken = default);
    Task<Content> Insert(string type, Content entity, CancellationToken cancellationToken = default);
    Task<Content?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<Content> Update(string type, Content entity, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}

public class ContentService : IContentService
{
    private readonly IContentTypeRepository _contentTypeRepository;
    private readonly IApplicationContext _applicationContext;
    private readonly IContentRepository _contentRepository;

    public ContentService(IContentRepository repository, IContentTypeRepository contentTypeRepository, IApplicationContext applicationContext)
    {
        _contentTypeRepository = contentTypeRepository;
        _applicationContext = applicationContext;
        _contentRepository = repository;
    }

    public Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        return _contentRepository.Delete(id, cancellationToken);
    }

    public async Task<List<Content>> GetAll(string type, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(type))
            return new List<Content>();

        var contentType = await _contentTypeRepository.GetByName(_applicationContext.SiteId, type, cancellationToken);

        return await _contentRepository.GetAll(contentType.Id, _applicationContext.SiteId, cancellationToken);
    }

    public Task<Content?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return _contentRepository.GetById(id, cancellationToken);
    }

    public async Task<Content> Insert(string type, Content entity, CancellationToken cancellationToken = default)
    {
        var typeDefinition = await _contentTypeRepository.GetByName(_applicationContext.SiteId, type, cancellationToken);

        entity.CreateBy = _applicationContext.UserId;
        entity.CreateDate = DateTime.Now;
        entity.LastUpdateDate = default;
        entity.LastUpdateBy = default;
        entity.SiteId = _applicationContext.SiteId;
        entity.TypeId = typeDefinition.Id;

        return await _contentRepository.Insert(entity, cancellationToken);
    }

    public async Task<Content> Update(string type, Content entity, CancellationToken cancellationToken = default)
    {
        var typeDefinition = await _contentTypeRepository.GetByName(_applicationContext.SiteId, type, cancellationToken);

        // todo: think about this
        //entity.CreateBy = _context.Username;
        //entity.CreateDate = DateTime.Now;
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdateBy = _applicationContext.UserId;
        entity.SiteId = _applicationContext.SiteId;
        entity.TypeId = typeDefinition.Id;

        return await _contentRepository.Update(entity, cancellationToken);
    }
}