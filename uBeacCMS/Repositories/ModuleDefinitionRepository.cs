using MongoDB.Bson;
using MongoDB.Driver;
using uBeac.Repositories;
using uBeac.Repositories.History;
using uBeacCMS.Models;

namespace uBeacCMS.Repositories;

public interface IModuleDefinitionRepository : IEntityRepository<ModuleDefinition>
{
    Task<ModuleDefinition> GetByName(string name, CancellationToken cancellationToken = default);
}
public class ModuleDefinitionRepository : MongoEntityRepository<ModuleDefinition, MongoDBContext>, IModuleDefinitionRepository
{
    public ModuleDefinitionRepository(MongoDBContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }

    public async Task<ModuleDefinition> GetByName(string name, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ModuleDefinition>.Filter.Regex(u => u.Name, new BsonRegularExpression("/^" + name + "$/i"));
        return (await Collection.FindAsync(filter)).SingleOrDefault();
    }
}
