using uBeac.Services;
using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IModuleDefinitionService : IEntityService<ModuleDefinition>
{

}
public class ModuleDefinitionService : EntityService<ModuleDefinition>, IModuleDefinitionService
{
    private readonly IModuleDefinitionRepository _repository;
    public ModuleDefinitionService(IModuleDefinitionRepository repository) : base(repository)
    {
        _repository = repository;
    }
}
