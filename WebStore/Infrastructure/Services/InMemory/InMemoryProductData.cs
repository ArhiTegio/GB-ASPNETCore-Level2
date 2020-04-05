using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        //класс-репозиторий напрямую обращается к контексту базы данных

        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null) => TestData.Products
            .Where(product => Filter == null || (Filter.SectionId == null || product.SectionId == Filter.SectionId))
            .Where(product => Filter == null || (Filter.BrandId == null || product.BrandId == Filter.BrandId));

        public Product GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id);

        public void SaveChanges()
        {
            //TestData.SaveChangesAsync();

        }
    }
}