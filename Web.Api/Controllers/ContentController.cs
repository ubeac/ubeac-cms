using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ContentController : ControllerBase
{
    private readonly IService<ContentDefinition> service;

    public ContentController(IContentService<BaseContent> service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<ContentDefinition> Insert([FromBody] ContentDefinition contentDefinition, CancellationToken cancellationToken = default)
    {
        return await service.Insert(contentDefinition, cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<ContentDefinition>> GetAll(CancellationToken cancellationToken = default)
    {
        return await service.GetAll(cancellationToken);
    }
}
