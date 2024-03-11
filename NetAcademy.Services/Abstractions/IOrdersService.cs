using NetAcademy.DataBase.Entities;

namespace NetAcademy.Services.Abstractions;

public interface IOrdersService
{
    OrderSmth? GetOrderById(int id);
    OrderSmth[] GetOrders();
    Task<Order[]> GetOrderHistory(Guid userId);


}