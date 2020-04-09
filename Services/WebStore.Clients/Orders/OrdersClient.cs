using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration, WebAPI.Orders) { }

        public IEnumerable<OrderDTO> GetUserOrders(string userName) =>
            Get<List<OrderDTO>>($"{_serviceAddress}/user/{userName}");

        public OrderDTO GetOrderById(int id) => Get<OrderDTO>($"{_serviceAddress}/{id}");

        public async Task<OrderDTO> CreateOrderAsync(string userName, CreateOrderModel orderModel)
        {
            var response = await PostAsync($"{_serviceAddress}/{userName}", orderModel);
            return await response.Content.ReadAsAsync<OrderDTO>();

        }
    }
}
