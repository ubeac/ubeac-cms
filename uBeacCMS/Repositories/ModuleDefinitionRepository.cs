using uBeac.Repositories;
using uBeac.Repositories.History;
using uBeacCMS.Models;

namespace uBeacCMS.Repositories;

public interface IModuleDefinitionRepository : IEntityRepository<ModuleDefinition>
{

}
public class ModuleDefinitionRepository : MongoEntityRepository<ModuleDefinition, MongoDBContext>, IModuleDefinitionRepository
{
    public ModuleDefinitionRepository(MongoDBContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }
}
