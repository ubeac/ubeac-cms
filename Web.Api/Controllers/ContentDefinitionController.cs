using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentDefinitionController : ControllerBase
{
    private readonly IService<ContentDefinition> service;

    public ContentDefinitionController(IService<ContentDefinition> service)
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

