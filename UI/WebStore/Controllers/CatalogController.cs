using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Infrastructure.Mapping;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;
using System.Collections.Generic;
using WebStore.Domain.DTO.Products;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private const string __PageSize = "PageSize";
        private readonly IProductData _productData;
        private readonly IConfiguration _Configuration;

        public CatalogController(IProductData ProductData, IConfiguration Configuration)
        {
            _productData = ProductData;
            _Configuration = Configuration;
        }

        public IActionResult Shop(int? sectionId, int? breandId, int page = 1)
        {
            var page_size = int.TryParse(_Configuration[__PageSize], out var size) ? size : (int?)null;

            var filter = new ProductFilter
            {
                SectionId = sectionId,
                BrandId = breandId,
                Page = page,
                PageSize = page_size
            };


            var products = _productData.GetProducts(filter);

            var answer = new CatalogViewModel()
            {
                SectionId = sectionId,
                BrandId = breandId,
                Products = products.Products.Select(ProductMapping.FromDTO).Select(ProductMapping.ToView).OrderBy(p => p.Order),
                PageView_Model = new PageViewModel
                {
                    PageSize = page_size ?? 0,
                    PageNumber = page,
                    TotalItems = products.TotalCount
                } 
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

        #region API

        public IActionResult GetFilteredItems(int? SectionId, int? BrandId, int Page)
        {
            var products =
                GetProducts(SectionId, BrandId, Page)
                   .Select(ProductMapping.FromDTO)
                   .Select(ProductMapping.ToView)
                   .OrderBy(p => p.Order);
            return PartialView("Partial/_FeaturesItems", products);
        }

        private IEnumerable<ProductDTO> GetProducts(int? SectionId, int? BrandId, int Page) =>
            _productData.GetProducts(new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Page = Page,
                PageSize = int.Parse(_Configuration[__PageSize])
            })
            .Products;

        #endregion
    }
}