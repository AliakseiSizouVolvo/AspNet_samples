using Microsoft.AspNetCore.Mvc;
using NetAcademy.Services.Abstractions;
using NetAcademy.UI.Models;

namespace NetAcademy.UI.Controllers
{
    public class OrdersController : Controller
    {
        //tight coupling sample
        //private readonly CustomerOrderService _customerOrderService;
        private readonly ICustomerOrderService _customerOrderService;
        private readonly IOrdersService _ordersService;

        public OrdersController(ICustomerOrderService customerOrderService, IOrdersService ordersService)
        {
            _customerOrderService = customerOrderService;
            _ordersService = ordersService;
        }
        
        public IActionResult Index()
        {
            var orders = _customerOrderService.GetOrdersOfCustomer();
            return View(orders);
        }

        public IActionResult Details(int id)//id
        {
            //todo should be part of service not a part of controller
            var order= _customerOrderService.GetOrderById(id);
            if (order != null)
            {
                return View(order);

            }
            return BadRequest();
        }

        public IActionResult OrderPreview()
        {
            var order = _customerOrderService.GetOrdersOfCustomer()
                .FirstOrDefault();

            return PartialView();
        }

        public async Task<IActionResult> OrdersHistory()
        {
            var orderHistoryEntities = await _ordersService.GetOrderHistory(Guid.Empty);//user Id

            var model = orderHistoryEntities.Select(order => new OrderViewModel()
            {
                Id = order.Id,
                State = order.State,
                OrderDate = order.OrderDate,
                TotalPrice = order.OrderItems.Sum(oi => oi.Book.Price * oi.Book.Price),
                OrderItems = order.OrderItems.Select(item => new OrderItemViewModel()
                {
                    BookName = item.Book.Name,
                    Quantity = item.Quantity
                }).ToArray()
            }).ToArray();

            return View(model);
        }
    }
}
