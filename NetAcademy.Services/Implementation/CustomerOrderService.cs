using NetAcademy.Services.Abstractions;

namespace NetAcademy.Services.Implementation;

public class CustomerOrderService : ICustomerOrderService
{
    private readonly IOrdersService _ordersService;

    public CustomerOrderService(IOrdersService orderService)
    {
        _ordersService = orderService;
    }


    public OrderSmth[] GetOrdersOfCustomer()
    {
        return _ordersService.GetOrders();
    }

    public OrderSmth? GetOrderById(int id)
    {
        return _ordersService.GetOrderById(id);
    }

    public string GetSomeLocalizedString(string key)
    {
        return "Hello";
    }
}