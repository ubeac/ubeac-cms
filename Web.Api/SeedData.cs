using Entities;
using Services;

public static class SeedData
{
    public static void SeedDefaultData(this IServiceProvider provider)
    {
        var scope = provider.CreateScope();


        var cmsContext = scope.ServiceProvider.GetRequiredService<CmsContext>();

        var siteService = scope.ServiceProvider.GetRequiredService<ISiteService>();
        var contentService = scope.ServiceProvider.GetRequiredService<IContentService>();
        var contentTypeService = scope.ServiceProvider.GetRequiredService<IContentTypeService>();
        
        if (!siteService.GetAll().GetAwaiter().GetResult().Any())
        {
            #region Site

            var site = new Site
            {
                Name = "Sample Website",
                Domain = "localhost:7101"
            };

            siteService.Insert(site).GetAwaiter().GetResult();

            cmsContext.SiteId = site.Id;

            #endregion

            #region Content Type

            var articleContentType = new ContentType
            {
                Name = "Article",
                Fields = new List<Field>
                      {
                          new Field
                          {
                               Label = "Title",
                               Name = "title",
                               DefaultValue = "Title",
                               Type = "text"
                          },
                          new Field
                          {
                               Label = "Description",
                               Name = "description",
                               DefaultValue = "",
                               Type = "texthtml"
                          }
                      }
            };
            var newsContentType = new ContentType
            {
                Name = "News",
                Fields = new List<Field>
                      {
                          new Field
                          {
                               Label = "Title",
                               Name = "title",
                               DefaultValue = "Title",
                               Type = "text"
                          },
                          new Field
                          {
                               Label = "Description",
                               Name = "description",
                               DefaultValue = "",
                               Type = "texthtml"
                          },
                          new Field
                          {
                               Label = "Publish Date",
                               Name = "publish_date",
                               DefaultValue = "",
                               Type = "date"
                          }
                      }
            };

            var contentTypes = new List<ContentType> { articleContentType, newsContentType };

            foreach (var contentType in contentTypes)
                contentTypeService.Insert(contentType).GetAwaiter().GetResult();

            #endregion

            #region Content - Articles

            var article1 = new Content
            {
                TypeId = articleContentType.Id
            };
            article1.Fields["title"] = "Article 1";
            article1.Fields["description"] = "Article 1 Description";

            var article2 = new Content
            {
                TypeId = articleContentType.Id
            };
            article2.Fields["title"] = "Article 2";
            article2.Fields["description"] = "Article 2 Description";

            var articles = new List<Content> { article1, article2 };
            foreach (var article in articles)
                contentService.Insert(articleContentType.Name, article).GetAwaiter().GetResult();

            #endregion

            #region Content - News

            var news1 = new Content
            {
                SiteId = site.Id,
                TypeId = newsContentType.Id
            };

            var news2 = new Content
            {
                SiteId = site.Id,
                TypeId = newsContentType.Id
            };

            var newsList = new List<Content> { news1, news2 };
            foreach (var news in newsList)
                contentService.Insert(newsContentType.Name, news).GetAwaiter().GetResult();

            #endregion

        }
    }
}
