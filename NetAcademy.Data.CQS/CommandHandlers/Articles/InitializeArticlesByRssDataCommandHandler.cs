using MediatR;
using Microsoft.EntityFrameworkCore;
using NetAcademy.Data.CQS.Commands.Articles;
using NetAcademy.DataBase;
using NetAcademy.DataBase.Entities;

namespace NetAcademy.Data.CQS.CommandHandlers.Articles
{
    public class InitializeArticlesByRssDataCommandHandler 
        : IRequestHandler<InitializeArticlesByRssDataCommand>
    {
        private readonly BookStoreDbContext _dbContext;

        public InitializeArticlesByRssDataCommandHandler
            (
                BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(InitializeArticlesByRssDataCommand command, CancellationToken cancellationToken)
        {
            var existedArticleLinks = await _dbContext.Articles
                .Select(article => article.SourceLink)
                .ToArrayAsync(cancellationToken);

            var articles = command.RssData.Select(item =>
                    new Article()
                    {
                        Id = Guid.NewGuid(),
                        Title = item.Title.Text,
                        Description = item.Summary.Text,
                        PublicationDate = item.PublishDate.UtcDateTime,
                        SourceLink = item.Links[0].Uri.ToString()
                    })
                .Where(art =>
                    !existedArticleLinks
                        .Contains(art.SourceLink))
                .ToArray();

            await _dbContext.Articles.AddRangeAsync(articles, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
