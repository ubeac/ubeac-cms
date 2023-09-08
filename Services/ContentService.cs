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

    public async Task Delete(string type, Guid id, CancellationToken cancellationToken = default)
    {

        var contentType = await _contentTypeRepository.GetByName(type, cancellationToken);

        await _contentRepository.Delete(contentType.Id, _context.SiteId, id, cancellationToken);
        return;
    }

    public async Task<List<Content>> GetAll(string type, CancellationToken cancellationToken = default)
    {
        var contentType = await _contentTypeRepository.GetByName(type, cancellationToken);
        return await _contentRepository.GetAll(contentType.Id, _context.SiteId, cancellationToken);
    }

    public async Task<Content?> GetById(string type, Guid id, CancellationToken cancellationToken = default)
    {
        var contentType = _contentTypeRepository.GetByName(type, cancellationToken).GetAwaiter().GetResult();
        return await _contentRepository.GetById(contentType.Id, _context.SiteId, id, cancellationToken);
    }

    public async Task<Content> Insert(string type, Content entity, CancellationToken cancellationToken = default)
    {
        var contentType = await _contentTypeRepository.GetByName(type, cancellationToken);

        entity.CreateBy = _context.Username;
        entity.CreateDate = DateTime.Now;
        entity.LastUpdateDate = default;
        entity.LastUpdateBy = default;
        entity.SiteId = _context.SiteId;
        entity.TypeId = contentType.Id;

        return await _contentRepository.Insert(contentType.Id, _context.SiteId, entity, cancellationToken);
    }

    public async Task<Content> Update(string type, Content entity, CancellationToken cancellationToken = default)
    {
        var contentType = await _contentTypeRepository.GetByName(type, cancellationToken);

        // todo: think about this
        //entity.CreateBy = _context.Username;
        //entity.CreateDate = DateTime.Now;
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdateBy = _context.Username;
        entity.SiteId = _context.SiteId;
        entity.TypeId = contentType.Id;

        return await _contentRepository.Update(contentType.Id, _context.SiteId, entity, cancellationToken);
    }
}