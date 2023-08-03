using uBeac.Repositories;
using uBeac.Repositories.History;
using uBeacCMS.Models;

namespace uBeacCMS.Repositories;

public interface IModuleDefinitionRepository : IEntityRepository<ModuleDefinition>
{

}
public class ModuleDefinitionRepository : MongoEntityRepository<ModuleDefinition, MainDBContext>, IModuleDefinitionRepository
{
    public ModuleDefinitionRepository(MainDBContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }
}
