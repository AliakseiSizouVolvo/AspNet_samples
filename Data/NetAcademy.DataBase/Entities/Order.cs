namespace NetAcademy.DataBase.Entities;

public class Order
{
    public Guid Id { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    public OrderState State { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public List<OrderItem> OrderItems { get; set; }
}