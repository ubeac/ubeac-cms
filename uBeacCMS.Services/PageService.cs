using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IPageService : IBaseService<Page> 
{
}

public class PageService:BaseService<Page>, IPageService
{
    public PageService(IBaseRepository<Page> repository):base(repository)
    {
            
    }
}
