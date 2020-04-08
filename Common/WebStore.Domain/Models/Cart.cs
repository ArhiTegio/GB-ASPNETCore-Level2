using System.Collections.Generic;
using System.Linq;

namespace WebStore.Domain.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public int ItemsCount => Items?.Sum(items => items.Quantity) ?? 0;
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
