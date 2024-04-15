using MediatR;
using Microsoft.EntityFrameworkCore;
using NetAcademy.Data.CQS.Queries.Articles;
using NetAcademy.DataBase;
using NetAcademy.DataBase.Entities;

namespace NetAcademy.Data.CQS.QueryHandlers.Articles;

public class GetArticlesWithTextQueryHandler :
    IRequestHandler<GetArticlesWithTextQuery,
        Article[]>
{
    private readonly BookStoreDbContext _dbContext;

    public GetArticlesWithTextQueryHandler(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Article[]> Handle(GetArticlesWithTextQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles
            .Include(article => article.Source)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }
}