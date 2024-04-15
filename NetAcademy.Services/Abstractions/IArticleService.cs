using NetAcademy.DataBase.Entities;
using NetAcademy.DTOs;

namespace NetAcademy.Services.Abstractions;

//will be changed in future
public interface IArticleService
{
    public Task<ArticleDto[]> GetArticlesAsync();
    public Task<Article?> GetArticlesByIdAsync(Guid id);

    //public Task<int> AddBookAsync(Book book);
    public Task AggregateFromSourceAsync(string rssLink, CancellationToken cancellationToken);
}