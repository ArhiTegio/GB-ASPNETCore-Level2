using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route(WebAPI.Products)]
    [ApiController]
    public class ProductApiController : ControllerBase, IProductData
    {
        private readonly IProductData _productData;

        public ProductApiController(IProductData productData) => _productData = productData;

        [HttpGet("sections")]
        public IEnumerable<Section> GetSections() => _productData.GetSections();

        [HttpGet("sections/{id}")]
        public SectionDTO GetSectionById(int id) => _productData.GetSectionById(id);

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands() => _productData.GetBrands();

        [HttpGet("brand/{id}")]
        public BrandDTO GetBrandById(int id) => _productData.GetBrandById(id);

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter Filter = null) => _productData.GetProducts(Filter);

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id) => _productData.GetProductById(id);
    }
}