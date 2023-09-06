using Entities;

namespace Repositories;

public interface IContentTypeDefinitionRepository : IBaseEntityRepository<ContentTypeDefinition>
{
    Task<ContentTypeDefinition> GetByName(string name, CancellationToken cancellationToken = default);
}
