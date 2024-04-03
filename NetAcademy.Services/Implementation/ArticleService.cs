using System.ServiceModel.Syndication;
using Microsoft.EntityFrameworkCore;
using NetAcademy.DataBase;
using NetAcademy.DataBase.Entities;
using NetAcademy.Services.Abstractions;
using System.Xml;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace NetAcademy.Services.Implementation;

public class ArticleService : IArticleService
{
    private readonly BookStoreDbContext _dbContext;
    private readonly ILogger<ArticleService> _logger;
    public ArticleService(BookStoreDbContext dbContext, ILogger<ArticleService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<Article[]> GetArticlesAsync()
    {
        return _dbContext.Articles.ToArrayAsync();
    }

    public Task<Article?> GetArticlesByIdAsync(Guid id)
    {
        return _dbContext.Articles.SingleOrDefaultAsync(article
            => article.Id.Equals(id));

    }

    public async Task AggregateFromSourceAsync(string rssLink)
    {
        try
        {
            var reader = XmlReader.Create(rssLink);
            var feed = SyndicationFeed.Load(reader);

            var existedArticles = await _dbContext.Articles
                .Select(article => article.SourceLink)
                .ToArrayAsync();

            var articles = feed.Items.Select(item =>
                new Article()
                {
                    Id = Guid.NewGuid(),
                    Title = item.Title.Text,
                    Description = item.Summary.Text,
                    PublicationDate = item.PublishDate.UtcDateTime,
                    SourceLink = item.Links[0].Uri.ToString()
                })
                .Where(art =>
                    !existedArticles
                        .Contains(art.SourceLink))
                .ToDictionary(article => article.SourceLink,
                    a => a);
            _logger.LogDebug("Articles was taken successfully");

            //Check possibility to parallel that code 
            // USUALLY SERVICES HAS SOME SECURITY
            // A lot of requests from 1 machine in short time looks like DDOS attack
            foreach (var article in articles)
            {
                var text = await GetArticleTextByUrl(article.Key);
                articles[article.Key].Text = text;
            }
            await _dbContext.Articles.AddRangeAsync(articles.Values);
            await _dbContext.SaveChangesAsync();

            _logger.LogDebug("Articles was added to DB successfully");
        }
        catch (Exception e)
        {
            throw;
        }
    }

    private async Task<string> GetArticleTextByUrl(string url)
    {
        var web = new HtmlWeb();
        var doc = await web.LoadFromWebAsync(url);

        var articleText = doc.DocumentNode
            .SelectSingleNode("//div[contains(@class, 'entry-content')]").InnerHtml;

        return articleText;
    }
}