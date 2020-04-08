

namespace WebStore.Domain.Entities.Interfaces
{
    /// <summary>Упорядочиваемая сущность</summary>
    public interface IOrderedEntity : IBaseEntity
    {
        /// <summary>Порядковый номер</summary>
        int Order { get; set; }
    }
}