using System.ComponentModel.DataAnnotations;

namespace ProductInventory.Domain.Common
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public T Id {get; set;}

        public BaseEntity()
        {
            if (typeof(T) == typeof(Guid))
            {
                Id = (T)(object)Guid.NewGuid();
            }
        }
    }
}
