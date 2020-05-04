using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Domain.ViewModels.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;
        public IActionResult Detals() => View(new CartOrderViewModel {CartViewModel = _cartService.TransformFromCart(), OrderViewModel = new OrderViewModel()});

        public IActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);
            return RedirectToAction(nameof(Detals));
        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);
            return RedirectToAction(nameof(Detals));
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Detals));
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction(nameof(Detals));
        }

        public async Task<IActionResult> CheckOut(OrderViewModel Model, [FromServices] IOrderService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Detals), new CartOrderViewModel
                {
                    CartViewModel = _cartService.TransformFromCart(),
                    OrderViewModel = Model
                });

            var order_model = new CreateOrderModel
            {
                OrderViewModel = Model,
                OrderItems = _cartService.TransformFromCart().Items
                    .Select(item => new OrderItemDTO
                    {
                        Id = item.Key.Id,
                        Price = item.Key.Price,
                        Quantity = item.Value
                    })
                    .ToList()
            };

            var order = await OrderService.CreateOrderAsync(User.Identity.Name, order_model);

            _cartService.RemoveAll();

            return RedirectToAction(nameof(OrderConfirmed), new { id = order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region API

        public IActionResult GetCartView() => ViewComponent("Cart");

        public IActionResult AddToCartAPI(int id)
        {
            _cartService.AddToCart(id);
            return Json(new { id, message = $"Товар id:{id} был добавлен в корзину" });
        }

        public IActionResult DecrementFromCartAPI(int id)
        {
            _cartService.DecrementFromCart(id);
            return Json(new { id, message = $"Количество товара с id:{id} было уменьшено на 1" });
        }

        public IActionResult RemoveFromCartAPI(int id)
        {
            _cartService.RemoveFromCart(id);
            return Json(new { id, message = $"Товар id:{id} был удалён из корзины" });
        }

        public IActionResult RemoveAllAPI()
        {
            _cartService.RemoveAll();
            return Json(new { message = "Корзина была успешно очищена" });
        }

        #endregion
    }
}