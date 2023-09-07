using Entities;
using Services;

namespace Controllers;

public class ContentDefinitionTypeController : BaseEntityController<ContentTypeDefinition>
{
    public ContentDefinitionTypeController(IBaseEntityService<ContentTypeDefinition> service) : base(service)
    {
    }
}