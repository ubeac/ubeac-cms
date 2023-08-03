using MongoDB.Driver;
using uBeac.Repositories;
using uBeac.Repositories.History;
using uBeacCMS.Models;

namespace uBeacCMS.Repositories;

public interface IModuleRepository : IEntityRepository<Module>
{
    Task<IEnumerable<Module>> GetByPageId(Guid pageId, CancellationToken cancellationToken = default);
}

public class ModuleRepository : MongoEntityRepository<Module, MongoDBContext>, IModuleRepository
{
    public ModuleRepository(MongoDBContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }

    public async Task<IEnumerable<Module>> GetByPageId(Guid pageId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Module>.Filter.Eq(x => x.PageId, pageId);
        return (await Collection.FindAsync(filter, cancellationToken: cancellationToken)).ToEnumerable();
    }
}
