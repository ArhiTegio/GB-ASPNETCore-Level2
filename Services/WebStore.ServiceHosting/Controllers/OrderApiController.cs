using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrderApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _orderService;

        public OrderApiController(IOrderService orderService) => _orderService = orderService;
        [HttpGet("user/{UserName}")]
        public IEnumerable<OrderDTO> GetUserOrders(string userName) => _orderService.GetUserOrders(userName);

        [HttpGet("{id}")]
        public OrderDTO GetOrderById(int id) => _orderService.GetOrderById(id);

        [HttpPost("{UserName?}")]
        public Task<OrderDTO> CreateOrderAsync(string userName, CreateOrderModel orderModel) => _orderService.CreateOrderAsync(userName, orderModel);
    }
}