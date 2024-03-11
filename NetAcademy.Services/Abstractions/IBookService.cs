using NetAcademy.DataBase.Entities;

namespace NetAcademy.Services.Abstractions;

//will be changed in future
public interface IBookService
{
    public Task<Book[]> GetBooksAsync();
    public Task<Book?> GetBookByIdAsync(Guid id);
    
    public Task<int> AddBookAsync(Book book);
}