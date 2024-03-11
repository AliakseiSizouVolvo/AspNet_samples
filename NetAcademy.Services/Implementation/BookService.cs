using Microsoft.EntityFrameworkCore;
using NetAcademy.DataBase;
using NetAcademy.DataBase.Entities;
using NetAcademy.Services.Abstractions;

namespace NetAcademy.Services.Implementation;

public class BookService : IBookService
{
    private readonly BookStoreDbContext _dbContext;
    //private readonly ReservedBookStoreDbContext _reserved;

    public BookService(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Book[]> GetBooksAsync()
    {
        //try
        //{
            return await _dbContext.Books.ToArrayAsync();
        //}
        //catch (Exception e)
        //{
        //    try
        //    {
        //        return _reserved.Books.ToArrayAsync();
        //    }
        //    catch (Exception exception)
        //    {
        //        throw;
        //    }
            
        //}
        
    }

    public Task<Book?> GetBookByIdAsync(Guid id)
    {
        return _dbContext.Books.SingleOrDefaultAsync(book => book.Id.Equals(id));
    }

    public async Task<int> AddBookAsync(Book book)
    {
        await _dbContext.AddAsync(book);
        return await _dbContext.SaveChangesAsync();
    }
}