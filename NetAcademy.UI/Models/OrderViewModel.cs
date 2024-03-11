using NetAcademy.DataBase.Entities;

namespace NetAcademy.UI.Models;

public class OrderViewModel
{
    public Guid Id { get; set; }
    public OrderState State { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    
    public OrderItemViewModel[] OrderItems { get; set; }
}