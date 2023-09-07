using Entities;
using Repositories;

namespace Services;

public interface IContentService
{
    Task<List<Content>> GetAll(string type, CancellationToken cancellationToken = default);
    Task<Content> Insert(string type, Content entity, CancellationToken cancellationToken = default);
    Task<Content?> GetById(string type, Guid id, CancellationToken cancellationToken = default);
    Task<Content> Update(string type, Content entity, CancellationToken cancellationToken = default);
    Task Delete(string type, Guid id, CancellationToken cancellationToken = default);
}

public class ContentService : IContentService
{
    private readonly IContentTypeRepository _contentTypeRepository;
    private readonly CmsContext _context;
    private readonly IContentRepository _contentRepository;

    public ContentService(IContentRepository repository, IContentTypeRepository contentTypeRepository, CmsContext context)
    {
        _contentTypeRepository = contentTypeRepository;
        _context = context;
        _contentRepository = repository;
    }

    public Task Delete(string type, Guid id, CancellationToken cancellationToken = default)
    {
        return _contentRepository.Delete(type, id, cancellationToken);
    }

    public Task<List<Content>> GetAll(string type, CancellationToken cancellationToken = default)
    {
        return _contentRepository.GetAll(type, _context.SiteId, cancellationToken);
    }

    public Task<Content?> GetById(string type, Guid id, CancellationToken cancellationToken = default)
    {
        return _contentRepository.GetById(type, id, cancellationToken);
    }

    public async Task<Content> Insert(string type, Content entity, CancellationToken cancellationToken = default)
    {
        var typeDefinition = await _contentTypeRepository.GetByName(type, cancellationToken);

        entity.CreateBy = _context.Username;
        entity.CreateDate = DateTime.Now;
        entity.LastUpdateDate = default;
        entity.LastUpdateBy = default;
        entity.SiteId = _context.SiteId;
        entity.TypeId = typeDefinition.Id;

        return await _contentRepository.Insert(type, entity, cancellationToken);
    }

    public async Task<Content> Update(string type, Content entity, CancellationToken cancellationToken = default)
    {
        var typeDefinition = await _contentTypeRepository.GetByName(type, cancellationToken);

        // todo: think about this
        //entity.CreateBy = _context.Username;
        //entity.CreateDate = DateTime.Now;
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdateBy = _context.Username;
        entity.SiteId = _context.SiteId;
        entity.TypeId = typeDefinition.Id;

        return await _contentRepository.Update(type, entity, cancellationToken);
    }
}