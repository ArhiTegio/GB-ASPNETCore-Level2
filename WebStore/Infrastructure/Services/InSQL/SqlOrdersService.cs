using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;
using WebStore.ViewModels.Orders;

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

        public IEnumerable<Order> GetUserOrders(string userName) => _db.Orders
            .Include(order => order.User)
            .Include(order => order.OrderItems)
            .Where(order => order.User.UserName == userName)
            .AsEnumerable();

        public Order GetOrderById(int id) => _db.Orders
            .Include(order => order.OrderItems)
            .FirstOrDefault(order => order.Id == id);

        public async Task<Order> CreateOrderAsync(string userName, CartViewModel cart, OrderViewModel orderModel)
        {
            var user = await _userManager.FindByNameAsync(userName);

            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                var order = new Order
                {
                    Name = orderModel.Name,
                    Address = orderModel.Adress,
                    Phone = orderModel.Phone,
                    User = user,
                    Date = DateTime.Now
                };
                await _db.AddAsync(order);
                foreach (var (product_model, quantity) in cart.Items)
                {
                    var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == product_model.Id);
                    if (product is null)
                        throw new InvalidOperationException($"Товар с id:{product_model.Id} в базе данных не найден.");

                    var item = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = quantity,
                        Product = product
                    };

                    await _db.OrderItems.AddAsync(item);
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return order;
            }
        }
    }
}
