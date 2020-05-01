using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration, string ServiceAddress) : base(configuration, WebAPI.Products) { }

        public IEnumerable<Section> GetSections() => Get<List<Section>>($"{_serviceAddress}/sections");
        public SectionDTO GetSectionById(int id) => Get<SectionDTO>($"{_serviceAddress}/sections/{id}");

        public IEnumerable<Brand> GetBrands() => Get<List<Brand>>($"{_serviceAddress}/brands");
        public BrandDTO GetBrandById(int id) => Get<BrandDTO>($"{_serviceAddress}/brands/{id}");

        public PagedProductsDTO GetProducts(ProductFilter Filter = null) =>
            Post(_serviceAddress, Filter ?? new ProductFilter())
                .Content
                .ReadAsAsync<PagedProductsDTO>()
                .Result;

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{_serviceAddress}/{id}");
    }
}
