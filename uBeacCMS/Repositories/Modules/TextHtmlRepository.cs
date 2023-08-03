using MongoDB.Driver;
using uBeac.Repositories;
using uBeac.Repositories.History;
using uBeacCMS.Models.Modules;

namespace uBeacCMS.Repositories;

public interface ITextHtmlRepository : IEntityRepository<TextHtml>
{
    Task<TextHtml> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default);
}
public class TextHtmlRepository : MongoEntityRepository<TextHtml, MongoDBContext>, ITextHtmlRepository
{
    public TextHtmlRepository(MongoDBContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }

    public async Task<TextHtml> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TextHtml>.Filter.Eq(x => x.ModuleId, moduleId);
        return (await Collection.FindAsync(filter, cancellationToken: cancellationToken)).SingleOrDefault();
    }
}
