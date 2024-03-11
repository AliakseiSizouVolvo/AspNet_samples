namespace NetAcademy.DataBase.Entities;

public class Review
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    public string ReviewText { get; set; }
    
    public DateTime ReviewDate { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid BookId { get; set; }
    public Book Book { get; set; }


}