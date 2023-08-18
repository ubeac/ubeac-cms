using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IModuleService : IBaseService<Module>
{
    Task<IList<Module>> GetByPageId(Guid pageId, CancellationToken cancellationToken = default);
}

public class ModuleService : BaseService<Module>, IModuleService
{
    public ModuleService(IBaseRepository<Module> repository) : base(repository)
    {

    }

    public async Task<IList<Module>> GetByPageId(Guid pageId, CancellationToken cancellationToken = default)
    {
        var modules = await BaseRepository.GetAll(cancellationToken).ConfigureAwait(false);
        return modules.Where(x => x.PageId == pageId).ToList();
    }

}
