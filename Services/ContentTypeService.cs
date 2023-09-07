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
}
