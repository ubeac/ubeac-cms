using uBeac.Services;
using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IModuleDefinitionService : IEntityService<ModuleDefinition>
{
    Task<ModuleDefinition> GetByName(string name, CancellationToken cancellationToken = default);
}
public class ModuleDefinitionService : EntityService<ModuleDefinition>, IModuleDefinitionService
{
    private readonly IModuleDefinitionRepository _repository;
    public ModuleDefinitionService(IModuleDefinitionRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public Task<ModuleDefinition> GetByName(string name, CancellationToken cancellationToken = default)
    {
        return _repository.GetByName(name, cancellationToken);
    }
        
}
