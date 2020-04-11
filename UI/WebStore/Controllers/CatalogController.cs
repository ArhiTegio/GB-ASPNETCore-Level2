using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Infrastructure.Mapping;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;
        public CatalogController(IProductData productData) => _productData = productData;

        public IActionResult Shop(int? sectionId, int? breandId)
        {
            var answer = new CatalogViewModel()
            {
                SectionId = sectionId,
                BrandId = breandId,
                Products = _productData.GetProducts(new ProductFilter() {BrandId = breandId, SectionId = sectionId})
                    .Select(product => new ProductViewModel()
                    {
                        Id = product.Id,
                        ImageUrl = product.ImageUrl,
                        Name = product.Name,
                        Order = product.Order,
                        Price = product.Price,
                        
                    }).OrderBy(product => product.Order),
            };

            return View(answer);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productData.GetProductById(id);
            if (product is null)
                return NotFound();

            return View(product.FromDTO().ToView());
        }
    }
}