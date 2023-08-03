using uBeac.Repositories;
using uBeac.Repositories.History;
using uBeacCMS.Models;

namespace uBeacCMS.Repositories;

public interface IModuleRepository : IEntityRepository<Module>
{

}
public class ModuleRepository : MongoEntityRepository<Module, MongoDBContext>, IModuleRepository
{
    public ModuleRepository(MongoDBContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }
}
