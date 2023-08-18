using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IModuleDefinitionService : IBaseService<ModuleDefinition>
{
    //Task<ModuleDefinition?> GetByViewType(string type, CancellationToken cancellationToken = default);
}

public class ModuleDefinitionService : BaseService<ModuleDefinition>, IModuleDefinitionService
{
    public ModuleDefinitionService(IBaseRepository<ModuleDefinition> repository) : base(repository)
    {

    }

    //public async Task<ModuleDefinition?> GetByViewType(string type, CancellationToken cancellationToken = default)
    //{
    //    var moduleDefs = await BaseRepository.GetAll(cancellationToken).ConfigureAwait(false);
    //    return moduleDefs.Where(x => x.ViewType == type).FirstOrDefault();
    //}
}
