using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Infrastructure.Mapping;
using WebStore.Interfaces.Services;

namespace WebStore.Infrastructure.Services.InCookies
{
    public class CookiesCartService : ICartService
    {
        private readonly string _CartName;
        private readonly IProductData _productData;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Cart CartUser
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                var cookies = context.Response.Cookies;
                var cart_cookie = context.Request.Cookies[_CartName];

                if (cart_cookie is null)
                {
                    var cart = new Cart();
                    cookies.Append(_CartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCookies(cookies, cart_cookie);
                return JsonConvert.DeserializeObject<Cart>(cart_cookie);
            }
            set => ReplaceCookies(_httpContextAccessor.HttpContext.Response.Cookies,
                JsonConvert.SerializeObject(value));
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_CartName);
            cookies.Append(_CartName, cookie, new CookieOptions {Expires = DateTime.Now.AddDays(15) });
        }

        public CookiesCartService(IProductData productData, IHttpContextAccessor httpContextAccessor)
        {
            _productData = productData;
            _httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext.User;
            var user_name = user.Identity.IsAuthenticated ? user.Identity.Name : null;
            _CartName = $"Cart {user_name}";
        }

        public void AddToCart(int id)
        {
            var cart = CartUser;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if(item is null)
                cart.Items.Add(new CartItem {ProductId = id, Quantity = 1});
            else
            {
                item.Quantity++;
            }

            CartUser = cart;
        }

        public void DecrementFromCart(int id)
        {
            var cart = CartUser;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null) return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity == 0)
                cart.Items.Remove(item);

            CartUser = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = CartUser;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null)
                return;
            cart.Items.Remove(item);

            CartUser = cart;
        }

        public void RemoveAll()
        {
            var cart = CartUser;
            cart.Items.Clear();
            CartUser = cart;
        }

        public CartViewModel TransformFromCart()
        {
            var product = _productData
                .GetProducts(new ProductFilter
                {
                    Ids = CartUser.Items.Select(item => item.ProductId).ToList()
                })
                .Products
                .Select(x => x.FromDTO())
                .ToView()
                .ToDictionary(p => p.Id);

            return new CartViewModel
            {
                Items = CartUser.Items
                    .Where(item => product.ContainsKey(item.ProductId))
                    .ToDictionary(
                    item => product[item.ProductId],
                    item => item.Quantity)
            };
        }
    }
}
