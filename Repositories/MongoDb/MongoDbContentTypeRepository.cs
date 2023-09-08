using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbContentTypeRepository : MongoDbBaseEntityRepository<ContentType>, IContentTypeRepository
{
    private readonly CmsContext _context;
    public MongoDbContentTypeRepository(IMongoDbProvider databaseProvider, CmsContext cmsContext) : base(databaseProvider)
    {
        _context = cmsContext;
    }

    public async Task<ContentType> GetByName(string name, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ContentType>.Filter.Eq(x => x.Name, name.ToLower());
        filter &= Builders<ContentType>.Filter.Eq(x => x.SiteId, _context.SiteId);

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.Single(cancellationToken: cancellationToken);
    }
}
