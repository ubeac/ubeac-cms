using Entities;
using Repositories;

namespace Services;

public interface IContentTypeService : IBaseEntityService<ContentType>
{
}

public class ContentTypeService : BaseEntityService<ContentType>, IContentTypeService
{
    public ContentTypeService(IContentTypeRepository repository, CmsContext cmsContext) : base(repository, cmsContext)
    {
    }
    public override Task<ContentType> Insert(ContentType entity, CancellationToken cancellationToken = default)
    {
        entity.SiteId = Context.SiteId;
        return base.Insert(entity, cancellationToken);
    }
}
