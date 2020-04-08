

namespace WebStore.Domain.Entities.Interfaces
{
    /// <summary>Именованная сущность</summary>
    public interface INamedEntity : IBaseEntity
    {
        /// <summary>Название</summary>
        string Name { get; set; }
    }
}