using uBeac.Services;
using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface ISiteService : IEntityService<Site>
{
    Task<Site> GetByDomain(string domain, CancellationToken cancellationToken = default);
}

public class SiteService : EntityService<Site>, ISiteService
{
    private readonly ISiteRepository _repository;
    public SiteService(ISiteRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public override Task Create(Site entity, CancellationToken cancellationToken = default)
    {
        PrepareSite(entity);
        return base.Create(entity, cancellationToken);
    }

    public override Task Update(Site entity, CancellationToken cancellationToken = default)
    {
        PrepareSite(entity);
        return base.Update(entity, cancellationToken);
    }

    private void PrepareSite(Site entity)
        => entity.Domains = entity.Domains.Select(x => x.ToLower()).ToList();


    public Task<Site> GetByDomain(string domain, CancellationToken cancellationToken = default)
        => _repository.GetByDomain(domain, cancellationToken);

}
