using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]/{contentType}/[action]")]
public class BaseContentController<TEntity> where TEntity : class, IBaseContent
{

    private readonly IBaseEntityRepository<ContentTypeDefinition> _contentTypeRepository;
    protected IBaseContentService<TEntity> Service { get; }

    public BaseContentController(IBaseContentService<TEntity> service, IBaseEntityRepository<ContentTypeDefinition> contentTypeRepository)
    {
        Service = service;
        _contentTypeRepository = contentTypeRepository;
    }

    [HttpPost]
    public Task Delete([FromBody] Guid id, CancellationToken cancellationToken = default)
    {
        return Service.Delete(id, cancellationToken);
    }

    [HttpGet]
    public async Task<List<TEntity>> GetAll([FromRoute] string contentType, CancellationToken cancellationToken = default)
    {
        var contentTypeDefinitions = await _contentTypeRepository.GetAll(x => x.Name.ToLower() == contentType.ToLower(), cancellationToken).ConfigureAwait(false);

        if (contentTypeDefinitions == null) throw new ArgumentException($"Content type {contentType} is not defined.");
        if (contentTypeDefinitions.Count > 1) throw new ArgumentException($"Content type {contentType} is defined multiple times.");

        return await Service.GetAll(contentTypeDefinition., cancellationToken);
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
