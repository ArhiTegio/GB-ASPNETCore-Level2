using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class ModelShopProduct
    {
        public string Pic { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }

        public ModelShopProduct() { }

        public ModelShopProduct(string pic, string name, string price)
        {
            Pic = pic;
            Name = name;
            Price = price;
        }
    }
}
