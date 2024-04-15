using MediatR;
using Microsoft.EntityFrameworkCore;
using NetAcademy.Data.CQS.Commands.Articles;
using NetAcademy.DataBase;
using NetAcademy.DataBase.Entities;

namespace NetAcademy.Data.CQS.CommandHandlers.Articles
{
    public class AddTextToArticlesCommandHandler : IRequestHandler<AddTextToArticlesCommand>
    {
        private readonly BookStoreDbContext _dbContext;

        public AddTextToArticlesCommandHandler
            (
                BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task Handle(AddTextToArticlesCommand command, CancellationToken cancellationToken)
        {
            var articles = await _dbContext.Articles
                .Where(article => command.ArticleTexts.ContainsKey(article.Id))
                .ToArrayAsync(cancellationToken);
            foreach (var article in articles)
            {
                article.Text = command.ArticleTexts[article.Id];
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
