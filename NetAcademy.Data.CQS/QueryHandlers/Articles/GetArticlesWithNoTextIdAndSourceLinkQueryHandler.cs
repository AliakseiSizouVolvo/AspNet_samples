using MediatR;
using Microsoft.EntityFrameworkCore;
using NetAcademy.Data.CQS.Queries.Articles;
using NetAcademy.DataBase;

namespace NetAcademy.Data.CQS.QueryHandlers.Articles;

public class GetArticlesWithNoTextIdAndSourceLinkQueryHandler:
    IRequestHandler<GetArticlesWithNoTextIdAndSourceLinkQuery, 
        Dictionary<Guid,string>>
{
    private readonly BookStoreDbContext _dbContext;

    public GetArticlesWithNoTextIdAndSourceLinkQueryHandler(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Dictionary<Guid, string>> Handle(GetArticlesWithNoTextIdAndSourceLinkQuery idAndSourceLinkQuery, CancellationToken cancellationToken)
    {
        var result = await
            _dbContext.Articles
                .AsNoTracking()
            .Where(article
                => !string.IsNullOrWhiteSpace(article.Text))
            .ToDictionaryAsync(art => art.Id,
                    art => art.SourceLink,
                cancellationToken: cancellationToken);

        return result;
    }
}