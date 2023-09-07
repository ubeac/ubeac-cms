using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/[controller]/{type}/[action]")]
[Produces("application/json")]
public class ContentController : ControllerBase
{
    protected IContentService Service { get; }

    public ContentController(IContentService service)
    {
        Service = service;
    }

    [HttpPost]
    public Task Delete([FromRoute] string type, [FromBody] Guid id, CancellationToken cancellationToken = default)
    {
        return Service.Delete(type, id, cancellationToken);
    }

    [HttpGet]
    public async Task<List<Content>> GetAll([FromRoute] string type, CancellationToken cancellationToken = default)
    {
        return await Service.GetAll(type, cancellationToken);
    }

    [HttpGet]
    public Task<Content?> GetById([FromRoute] string type, [FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        return Service.GetById(type, id, cancellationToken);
    }

    [HttpPost]
    public Task<Content> Insert([FromRoute] string type, [FromBody] Content content, CancellationToken cancellationToken = default)
    {
        return Service.Insert(type, content, cancellationToken);
    }

    [HttpPost]
    public async Task<Content> Update([FromRoute] string type, [FromBody] Content entity, CancellationToken cancellationToken = default)
    {
        return await Service.Update(type, entity, cancellationToken);
    }
}
