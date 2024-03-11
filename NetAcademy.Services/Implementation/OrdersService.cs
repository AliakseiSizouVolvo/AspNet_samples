using Microsoft.EntityFrameworkCore;
using NetAcademy.DataBase;
using NetAcademy.DataBase.Entities;
using NetAcademy.Services.Abstractions;

namespace NetAcademy.Services.Implementation
{
    public class OrdersService : IOrdersService
    {
        
        private readonly Dictionary<int, OrderSmth> _orders;
        private readonly BookStoreDbContext _dbContext;
        public OrdersService(BookStoreDbContext dbContext /*Dictionary<int, OrderSmth> dict*/)
        {
            _dbContext = dbContext;
            //_orders = dict;
            _orders = new Dictionary<int, OrderSmth>()
            {
                {
                    1,
                    new OrderSmth()
                    {
                        OrderId = 1,
                        OrderData = "OrderSmth 1",
                        Address = "Some specific Address",
                        OrderDate = DateTime.Today.Subtract(new TimeSpan(5,0,0,0)),
                        OrderSendTime = DateTime.Today,
                        OrderComment = "Some comment",
                        CustomerFullname = "Full name"
                    }},
                {
                    2,
                    new OrderSmth()
                    {
                        OrderId = 2,
                        OrderData = "OrderSmth 2",

                        Address = "Some specific Address",
                        OrderDate = DateTime.Today.Subtract(new TimeSpan(5,0,0,0)),
                        OrderSendTime = DateTime.Today,
                        OrderComment = "Some comment",
                        CustomerFullname = "Full name"
                    }},

                {
                    3,
                    new OrderSmth()
                    {
                        OrderId = 3,
                        OrderData = "OrderSmth 3",
                        Address = "Some specific Address",
                        OrderDate = DateTime.Today.Subtract(new TimeSpan(5,0,0,0)),
                        OrderSendTime = DateTime.Today,
                        OrderComment = "Some comment",
                        CustomerFullname = "Full name"
                    }},

                {
                    4,
                    new OrderSmth()
                    {
                        OrderId = 4,
                        OrderData = "OrderSmth 4",
                        Address = "Some specific Address",
                        OrderDate = DateTime.Today.Subtract(new TimeSpan(5,0,0,0)),
                        OrderSendTime = DateTime.Today,
                        OrderComment = "Some comment",
                        CustomerFullname = "Full name"
                    }},
                {
                    5,
                    new OrderSmth()
                    {
                        OrderId = 5,
                        OrderData = "OrderSmth 5",
                        Address = "Some specific Address",
                        OrderDate = DateTime.Today.Subtract(new TimeSpan(5,0,0,0)),
                        OrderSendTime = DateTime.Today,
                        OrderComment = "Some comment",
                        CustomerFullname = "Full name"
                    }},
                {
                    6,
                    new OrderSmth()
                    {
                        OrderId = 6,
                        OrderData = "OrderSmth 6",
                        Address = "Some specific Address",
                        OrderDate = DateTime.Today.Subtract(new TimeSpan(5,0,0,0)),
                        OrderSendTime = DateTime.Today,
                        OrderComment = "Some comment",
                        CustomerFullname = "Full name"
                    }},
                {
                    7,
                    new OrderSmth()
                    {
                        OrderId = 7,
                        OrderData = "OrderSmth 7",
                        Address = "Some specific Address",
                        OrderDate = DateTime.Today.Subtract(new TimeSpan(5,0,0,0)),
                        OrderSendTime = DateTime.Today,
                        OrderComment = "Some comment",
                        CustomerFullname = "Full name"
                    }},
            };
        }

        public OrderSmth[] GetOrders()
        {
            return _orders.Values.ToArray();
        }

        public async Task<Order[]> GetOrderHistory(Guid userId)
        {
            if (userId.Equals(Guid.Empty))
            {
                var orderMock = new List<Order>()
                {
                    new Order()
                    {
                        Id = Guid.NewGuid(),
                        OrderDate = DateTime.Today,
                        State = OrderState.Payed,
                        OrderItems = new List<OrderItem>()
                        {
                            new OrderItem()
                            {
                                Quantity = 2,
                                Book = new Book() { Name = "Book 1", Price = 5 }
                            },
                            new OrderItem()
                            {
                                Quantity = 5,
                                Book = new Book() { Name = "Book 2", Price = 15 }
                            },
                        }
                    },
                    new Order()
                    {
                        Id = Guid.NewGuid(),
                        OrderDate = DateTime.Today,
                        State = OrderState.Cancelled,
                        OrderItems = new List<OrderItem>()
                        {
                            new OrderItem()
                            {
                                Quantity = 3,
                                Book = new Book() { Name = "Book 3", Price = 10 }
                            },
                            new OrderItem()
                            {
                                Quantity = 1,
                                Book = new Book() { Name = "Book 4", Price = 20 }
                            },
                        }
                    },
                };
                return orderMock.ToArray();
            }
            var orders = await _dbContext.Orders
                .Where(o => o.UserId.Equals(userId))
                .Include(order => order.OrderItems)
                .ThenInclude(item => item.Book)
                .AsNoTracking()
                .ToArrayAsync();

            return orders;
        }

        public OrderSmth? GetOrderById(int id)
        {
            return _orders.ContainsKey(id) 
                ? _orders[id] 
                : null;
        }
    }
}
