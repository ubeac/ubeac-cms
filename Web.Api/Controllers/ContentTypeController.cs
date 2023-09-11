using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]

public class ContentTypeController : ControllerBase
{
    private readonly IContentTypeService _contentTypeService;

    public ContentTypeController(IContentTypeService contentTypeService)
    {
        _contentTypeService = contentTypeService;
    }

    [HttpPost]
    public Task Delete([FromBody] Guid id, CancellationToken cancellationToken = default)
    {
        return _contentTypeService.Delete(id, cancellationToken);
    }

    [HttpGet]
    public async Task<IList<ContentType>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _contentTypeService.GetAll(cancellationToken);
    }

    [HttpGet]
    public Task<ContentType?> GetById([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        return _contentTypeService.GetById(id, cancellationToken);
    }


    [HttpGet]
    public Task<ContentType> GetByName([FromQuery] string type, CancellationToken cancellationToken = default)
    {
        return _contentTypeService.GetByName(type, cancellationToken);
    }

    [HttpPost]
    public Task<ContentType> Insert([FromBody] ContentType entity, CancellationToken cancellationToken = default)
    {
        return _contentTypeService.Insert(entity, cancellationToken);
    }

    [HttpPost]
    public async Task<ContentType> Update([FromBody] ContentType entity, CancellationToken cancellationToken = default)
    {
        return await _contentTypeService.Update(entity, cancellationToken);
    }
}