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

    public Task<TextHtml> GetByModuleId(Guid moduleId, CancellationToken cancellationToken = default)
    {
        return _repository.GetByModuleId(moduleId, cancellationToken);
    }
}
