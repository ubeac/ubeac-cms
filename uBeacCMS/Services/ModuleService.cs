using uBeac.Services;
using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IModuleService : IEntityService<Module>
{
    Task<IEnumerable<Module>> GetByPageId(Guid pageId, CancellationToken cancellationToken = default);
}

public class ModuleService : EntityService<Module>, IModuleService
{
    private readonly IModuleRepository _repository;
    public ModuleService(IModuleRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Module>> GetByPageId(Guid pageId, CancellationToken cancellationToken = default)
    {
        return _repository.GetByPageId(pageId, cancellationToken);
    }        
}
