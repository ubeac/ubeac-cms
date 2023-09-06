using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]/{contentType}/[action]")]
[Produces("application/json")]
public class BaseContentController<TEntity> : ControllerBase where TEntity : class, IBaseContent
{

    protected IBaseContentService<TEntity> Service { get; }

    public BaseContentController(IBaseContentService<TEntity> service)
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
        return Service.Insert(entity, cancellationToken);
    }

    [HttpPost]
    public async Task<TEntity> Update([FromRoute] string contentType, [FromBody] TEntity entity, CancellationToken cancellationToken = default)
    {
        return await Service.Update(entity, cancellationToken);
    }

}
