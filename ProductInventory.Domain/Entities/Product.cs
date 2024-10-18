using ProductInventory.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ProductInventory.Domain.Entities
{
    public class Product : AuditableBaseEntity<Guid>
    {
        [MaxLength(25)]
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
