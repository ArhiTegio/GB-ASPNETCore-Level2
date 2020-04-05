using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Interfaces;

namespace WebStore.Domain.Entities
{
    /// <summary>
    /// Секция товаров
    /// </summary>
    //[Table("Sections")]
    public class Section: NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Номер заказа
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Индентификатор родителя
        /// </summary>
        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Section ParentSection { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
