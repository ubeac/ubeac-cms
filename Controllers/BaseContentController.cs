using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]/{contentType}/[action]")]
public class BaseContentController<TEntity> where TEntity : class, IBaseContent
{

    protected IBaseContentService<TEntity> Service { get; }

    public BaseContentController(IBaseContentService<TEntity> service, IBaseEntityRepository<ContentTypeDefinition> contentTypeRepository)
    {
        Service = service;
    }

    [HttpPost]
    public Task Delete([FromBody] Guid id, CancellationToken cancellationToken = default)
    {
        return Service.Delete(id, cancellationToken);
    }

    [HttpGet]
    public async Task<List<TEntity>> GetAll([FromRoute] string contentType, CancellationToken cancellationToken = default)
    {
        return await Service.GetAll(contentType, cancellationToken);
    }

    [HttpGet]
    public Task<TEntity> GetById([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        return Service.GetById(id, cancellationToken);
    }

    [HttpPost]
    public Task<TEntity> Insert([FromRoute] string contentType, [FromBody] TEntity entity, CancellationToken cancellationToken = default)
    {
        return Service.Insert(contentType, entity, cancellationToken);
    }

    [HttpPost]
    public async Task<TEntity> Update([FromBody] TEntity entity, CancellationToken cancellationToken = default)
    {
        return await Service.Update(entity, cancellationToken);
    }

}
