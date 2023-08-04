using MongoDB.Driver;
using uBeac.Repositories;
using uBeac.Repositories.History;

using uBeacCMS.Models.Modules;

namespace uBeacCMS.Repositories;

public interface IImageRepository : IEntityRepository<Image> 
{
    Task<Image> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default);
}

public class ImageRepository : MongoEntityRepository<Image, MongoDBContext>, IImageRepository
{
    public ImageRepository(MongoDBContext mongoDbContext, IApplicationContext appCtx, IHistoryManager historyManager) : base(mongoDbContext, appCtx, historyManager)
    {

    }

    public async Task<Image> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default) {
        var filter = Builders<Image>.Filter.Eq(x => x.ModuleId, moduleId);

        return (await Collection.FindAsync(filter, cancellationToken: cancellationToken)).SingleOrDefault();
    }
}