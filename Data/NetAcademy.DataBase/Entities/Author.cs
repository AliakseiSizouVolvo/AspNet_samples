namespace NetAcademy.DataBase.Entities;

public class Author
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public DateTime? DeathDate { get; set; }
    public string Biography { get; set; }
    
    public List<BookAuthor> AuthorBooks { get; set; }

}