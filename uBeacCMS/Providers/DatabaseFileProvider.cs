﻿using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System.Text;
using uBeacCMS.Models;
using uBeacCMS.Services;

namespace uBeacCMS.Providers;

public class DatabaseFileProvider : IFileProvider
{
    private readonly IPageService _pageService;
    private readonly ISiteService _siteService;

    public DatabaseFileProvider(ISiteService siteService, IPageService pageService)
    {
        _pageService = pageService;
        _siteService = siteService;
    }

    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        //throw new NotImplementedException();
        return null;
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        subpath = subpath.Replace("/Pages/", string.Empty);
        subpath = subpath.Replace("/", string.Empty);
        subpath = subpath.Replace(".cshtml", string.Empty);

        if (!Guid.TryParse(subpath, out var pageId))
            return new NotFoundFileInfo(subpath);

        var page = _pageService.GetById(pageId).Result;

        if (page == null)
            return new NotFoundFileInfo(subpath);

        if (string.IsNullOrEmpty(page.Template))
            page.Template = _siteService.GetById(page.SiteId).Result?.Template;        

        return new DatabaseFileInfo(page);
    }

    public IChangeToken Watch(string filter)
    {
        return new DatabaseChangeToken(filter);
    }
}


public class DatabaseFileInfo : IFileInfo
{
    private readonly Page page;
    private readonly byte[] viewContent;

    public DatabaseFileInfo(Page page)
    {
        this.page = page;
        viewContent = Encoding.UTF8.GetBytes(page.Template);
    }

    public bool Exists => true;

    public bool IsDirectory => false;

    public DateTimeOffset LastModified => page.LastUpdatedAt;

    public long Length
    {
        get
        {
            using var stream = new MemoryStream(viewContent);
            return stream.Length;
        }
    }

    public string Name => Path.GetFileName(page.Url);

    public string PhysicalPath => null;

    public Stream CreateReadStream()
    {
        return new MemoryStream(viewContent);
    }

}

public class DatabaseChangeToken : IChangeToken
{
    private readonly string subpath;

    public DatabaseChangeToken(string subpath)
    {
        this.subpath = subpath;
    }

    public bool ActiveChangeCallbacks => false;

    public bool HasChanged
    {
        get
        {
            return false;
            //var page = dbContext.Pages.Where(x => x.Url.ToLower() == subpath.ToLower()).FirstOrDefault();
            //if (page is not null && page.LastRequested.HasValue && page.LastRequested < page.LastModified)
            //    return true;

            //return false;
        }
    }

    public IDisposable RegisterChangeCallback(Action<object> callback, object state) => EmptyDisposable.Instance;
}

internal class EmptyDisposable : IDisposable
{
    public static EmptyDisposable Instance { get; } = new EmptyDisposable();
    private EmptyDisposable() { }
    public void Dispose() { }
}