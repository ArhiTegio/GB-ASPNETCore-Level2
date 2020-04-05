using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Domain.ViewModels.Orders;

namespace WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string userName);

        Order GetOrderById(int id);

        Task<Order> CreateOrderAsync(string userName, CartViewModel cart, OrderViewModel order);
    }
}
