using uBeac.Services;
using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface ISiteService : IEntityService<Site>
{

}
public class SiteService : EntityService<Site>, ISiteService
{
    private readonly ISiteRepository _repository;
    public SiteService(ISiteRepository repository) : base(repository)
    {
        _repository = repository;
    }
}
