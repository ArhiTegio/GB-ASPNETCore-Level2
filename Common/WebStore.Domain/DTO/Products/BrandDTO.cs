﻿using WebStore.Domain.Entities.Interfaces;

namespace WebStore.Domain.DTO.Products
{
    public class BrandDTO : INamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
