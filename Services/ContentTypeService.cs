using Entities;
using Repositories;

namespace Services;

public interface IContentTypeService : IBaseEntityService<ContentType>
{
    public Task<ContentType> GetByName(string name, CancellationToken cancellationToken = default);
}

public class ContentTypeService : BaseEntityService<ContentType>, IContentTypeService
{
    private IContentTypeRepository _repository { get; set; }

    public ContentTypeService(IContentTypeRepository repository, IApplicationContext applicationContext) : base(repository, applicationContext)
    {
        _repository = repository;
    }

    public Task<ContentType> GetByName(string name, CancellationToken cancellationToken = default)
    {
        return _repository.GetByName(Context.SiteId, name, cancellationToken);
    }


    public override Task<ContentType> Insert(ContentType entity, CancellationToken cancellationToken = default)
    {
        entity.SiteId = Context.SiteId;
        return base.Insert(entity, cancellationToken);
    }

    public override Task<ContentType> Update(ContentType entity, CancellationToken cancellationToken = default)
    {
        entity.SiteId = Context.SiteId;
        return base.Update(entity, cancellationToken);
    }

}
