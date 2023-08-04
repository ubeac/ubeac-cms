using System.Web;
using uBeac.Services;
using uBeacCMS.Models.Modules;
using uBeacCMS.Repositories;


namespace uBeacCMS.Services;


public interface IImageService: IEntityService<Image> 
{
    Task<Image> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default); 
} 

public class ImageService: EntityService<Image>, IImageService 
{
    private readonly IImageRepository _repository;
    public ImageService(IImageRepository repository) : base(repository){ 

        _repository = repository;
    }
    
    public Task<Image> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default) {
        return _repository.GetByModuleId(moduleId, cancellationToken);
    }
}