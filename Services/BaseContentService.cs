﻿using Entities;
using Repositories;

namespace Services;

public interface IBaseContentService<TEntity> : IBaseEntityService<TEntity> where TEntity : class, IBaseContent
{
    Task<List<TEntity>> GetAll(string typeName, CancellationToken cancellationToken = default);
}

public class BaseContentService<TEntity> : BaseEntityService<TEntity>, IBaseContentService<TEntity> where TEntity : class, IBaseContent
{
    private readonly IContentTypeDefinitionRepository _contentTypeDefinitionRepository;
    private readonly CmsContext _context;
    private readonly IBaseContentRepository<TEntity> _baseContentRepository;

    public BaseContentService(IBaseContentRepository<TEntity> repository, IContentTypeDefinitionRepository contentTypeDefinitionRepository, CmsContext context) : base(repository)
    {
        _contentTypeDefinitionRepository = contentTypeDefinitionRepository;
        _context = context;
        _baseContentRepository = repository;
    }

    public async Task<List<TEntity>> GetAll(string typeName, CancellationToken cancellationToken = default)
    {
        var contentTypeDefinition = await _contentTypeDefinitionRepository.GetByName(typeName, cancellationToken).ConfigureAwait(false);
        return await _baseContentRepository.GetAll(_context.SiteId, contentTypeDefinition.Id, cancellationToken);
    }
}