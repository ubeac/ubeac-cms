using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface ISkinService : IBaseService<Skin>
{
    Task<IList<Skin>> GetByIds(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}

public class SkinService : BaseService<Skin>, ISkinService
{
    public SkinService(IBaseRepository<Skin> repository) : base(repository)
    {
    }

    public async Task<IList<Skin>> GetByIds(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var allSkins = await BaseRepository.GetAll(cancellationToken);
        return allSkins.Where(x => ids.Contains(x.Id)).ToList();
    }
}