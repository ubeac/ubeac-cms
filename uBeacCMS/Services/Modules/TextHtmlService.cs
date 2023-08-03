using System.Web;
using uBeac.Services;
using uBeacCMS.Models.Modules;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface ITextHtmlService : IEntityService<TextHtml>
{
    Task<TextHtml> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default);
}

public class TextHtmlService : EntityService<TextHtml>, ITextHtmlService
{
    private readonly ITextHtmlRepository _repository;
    public TextHtmlService(ITextHtmlRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public override Task Create(TextHtml entity, CancellationToken cancellationToken = default)
    {
        entity.Content = HttpUtility.HtmlEncode(entity.Content);
        return base.Create(entity, cancellationToken);
    }

    public override async Task<TextHtml> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await base.GetById(id, cancellationToken);
        result.Content = HttpUtility.HtmlDecode(result.Content);
        return result;
    }

    public Task<TextHtml> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default)
    {
        return _repository.GetByModuleId(moduleId, cancellationToken);
    }
}
