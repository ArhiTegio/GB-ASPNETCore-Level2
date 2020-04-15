﻿using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Interfaces;

namespace WebStore.Domain.Entities.Base
{
    /// <summary>Именованная сущность</summary>
    public abstract class NamedEntity : BaseEntity, INamedEntity
    {
        /// <summary>Имя</summary>
        [Required/*, StringLength(250)*/]
        public string Name { get; set; }
    }
}