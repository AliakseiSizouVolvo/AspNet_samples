namespace NetAcademy.Services.Abstractions;

public interface ICustomerOrderService
{
    OrderSmth[] GetOrdersOfCustomer();
    OrderSmth? GetOrderById(int id);

    string GetSomeLocalizedString(string key);
}