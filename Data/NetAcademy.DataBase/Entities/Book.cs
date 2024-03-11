namespace NetAcademy.DataBase.Entities;

public class Book
{
    public Guid Id { get; set; }
    
    public string ISBN { get; set; }
    
    public string Name { get; set; }
    
    public int Year { get; set; }
    
    public decimal Price { get; set; }
    
    public List<BookAuthor> BookAuthors { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public List<Review> Reviews { get; set; }
}