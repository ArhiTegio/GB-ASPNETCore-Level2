using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Mapping;
using WebStore.Interfaces.Services;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData _productData;

        public ProductsController(IProductData productData) => _productData = productData;

        public IActionResult Index([FromServices] IMapper Mapper) =>
            View(_productData.GetProducts().Products.Select(x => Mapper.Map<Product>(x))); //x.FromDTO()));

        public IActionResult Edit(int? id, [FromServices] IMapper Mapper)
        {
            var product = id is null ? new Product() : Mapper.Map<Product>(_productData.GetProductById((int)id)); // _productData.GetProductById((int) id).FromDTO();

            if (product is null)
                return NotFound();

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _productData.GetProductById(id);

            if (product is null)
                return NotFound();

            return View(product.FromDTO());
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName(nameof(Delete))]
        public IActionResult DeleteConfirm(int id) => RedirectToAction(nameof(Index));

    }
}