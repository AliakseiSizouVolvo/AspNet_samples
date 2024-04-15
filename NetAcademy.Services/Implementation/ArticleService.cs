using System.ComponentModel;
using System.ServiceModel.Syndication;
using Microsoft.EntityFrameworkCore;
using NetAcademy.DataBase;
using NetAcademy.DataBase.Entities;
using NetAcademy.Services.Abstractions;
using System.Xml;
using HtmlAgilityPack;
using MediatR;
using Microsoft.Extensions.Logging;
using NetAcademy.Data.CQS.CommandHandlers.Articles;
using NetAcademy.Data.CQS.Commands.Articles;
using NetAcademy.Data.CQS.Queries.Articles;
using NetAcademy.Data.CQS.QueryHandlers.Articles;
using NetAcademy.DTOs;
using NetAcademy.UI.Mapper;

namespace NetAcademy.Services.Implementation;

public class ArticleService : IArticleService
{
    private readonly BookStoreDbContext _dbContext;
    private readonly ILogger<ArticleService> _logger;
    private readonly IMediator _mediator;
    private readonly ArticleMapper _articleMapper;
    
    public ArticleService(BookStoreDbContext dbContext, ILogger<ArticleService> logger, IMediator mediator, ArticleMapper articleMapper)
    {
        //_dbContext = dbContext;
        _dbContext = dbContext;
        _logger = logger;
        _mediator = mediator;
        _articleMapper = articleMapper;
    }

    public async Task<ArticleDto[]> GetArticlesAsync()
    {
        var articleArr = 
            (await _mediator.Send(new GetArticlesWithTextQuery()))
            .Select(article => _articleMapper.ArticleToArticleDto(article))
            .ToArray();
        return articleArr;
    }

    public Task<Article?> GetArticlesByIdAsync(Guid id)
    {
        return null;

        //return _dbContext.Articles.SingleOrDefaultAsync(article
        //    => article.Id.Equals(id));

    }

    public async Task AggregateFromSourceAsync(string rssLink, CancellationToken cancellationToken)
    {
        try
        {
            var reader = XmlReader.Create(rssLink);
            var feed = SyndicationFeed.Load(reader);


            await _mediator.Send(new InitializeArticlesByRssDataCommand()
            {
                RssData = feed.Items
            }, cancellationToken);
            _logger.LogDebug("Articles was taken successfully");


            var articlesWithNoText = await _mediator.Send(
                new GetArticlesWithNoTextIdAndSourceLinkQuery(),
                cancellationToken);
            //Check possibility to parallel that code 
            // USUALLY SERVICES HAS SOME SECURITY
            // A lot of requests from 1 machine in short time looks like DDOS attack
            var data = new Dictionary<Guid, string>();

            foreach (var article in articlesWithNoText)
            {
                var text = await GetArticleTextByUrl(article.Value);
                data.Add(article.Key, text);
            }
            //}

                //        _logger.LogDebug("Articles was added to DB successfully");
                //}
                await _mediator.Send(new AddTextToArticlesCommand()
                {
                    ArticleTexts = data
                }, cancellationToken);
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