using uBeac.Services;
using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IPageService : IEntityService<Page>
{

}
public class PageService : EntityService<Page>, IPageService
{
    private readonly IPageRepository _repository;
    public PageService(IPageRepository repository) : base(repository)
    {
        _repository = repository;
    }
}
