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
    {
        for (int i = 0; i < entity.Domains.Count; i++)
        {
            entity.Domains[i] = entity.Domains[i].ToLower();
        }
    }

    public Task<Site> GetByDomain(string domain, CancellationToken cancellationToken = default)
    {
        return _repository.GetByDomain(domain, cancellationToken);
    }
}
