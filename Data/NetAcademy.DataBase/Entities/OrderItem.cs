namespace NetAcademy.DataBase.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}