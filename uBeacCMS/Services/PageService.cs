﻿using uBeac.Services;
using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IPageService : IEntityService<Page>
{
    Task<Page> GetByUrl(string url, CancellationToken cancellationToken = default);

}
public class PageService : EntityService<Page>, IPageService
{

    private readonly IPageRepository _repository;

    public PageService(IPageRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public override Task Create(Page entity, CancellationToken cancellationToken = default)
    {
        entity.Url = entity.Url.ToLower();
        return base.Create(entity, cancellationToken);
    }

    public override Task Update(Page entity, CancellationToken cancellationToken = default)
    {
        entity.Url = entity.Url.ToLower();
        return base.Update(entity, cancellationToken);
    }

    public Task<Page> GetByUrl(string url, CancellationToken cancellationToken = default)
    {
        if (url.StartsWith("/"))
            url = url.Substring(1);

        if (url.EndsWith("/"))
            url = url.Remove(url.Length - 1);

        return _repository.GetByUrl(url, cancellationToken);
    }

}
