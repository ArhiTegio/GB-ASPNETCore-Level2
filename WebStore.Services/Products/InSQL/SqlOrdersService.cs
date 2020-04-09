using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Domain.ViewModels.Orders;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlOrdersService : IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _userManager;

        public SqlOrdersService(WebStoreDB db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IEnumerable<OrderDTO> GetUserOrders(string userName) => _db.Orders
            .Include(order => order.User)
            .Include(order => order.OrderItems)
            .Where(order => order.User.UserName == userName).Select(x => x.ToDTO())
            .AsEnumerable();

        public OrderDTO GetOrderById(int id) => _db.Orders
            .Include(order => order.OrderItems)
            .FirstOrDefault(order => order.Id == id).ToDTO();

        public async Task<OrderDTO> CreateOrderAsync(string userName, CreateOrderModel orderModel)
        {
            var user = await _userManager.FindByNameAsync(userName);

            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                var order = new Order
                {
                    Name = orderModel.OrderViewModel.Name,
                    Address = orderModel.OrderViewModel.Adress,
                    Phone = orderModel.OrderViewModel.Phone,
                    User = user,
                    Date = DateTime.Now
                };
                await _db.AddAsync(order);
                foreach (var item in orderModel.OrderItems)
                {
                    var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == item.Id);
                    if (product is null)
                        throw new InvalidOperationException($"Товар с id:{item.Id} в базе данных не найден.");

                    var orderItem = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = item.Quantity,
                        Product = product
                    };

                    await _db.OrderItems.AddAsync(orderItem);
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return order.ToDTO();
            }
        }
    }
}
