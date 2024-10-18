using ProductInventory.Domain.Common.Interfaces;

namespace ProductInventory.Domain.Common
{
    public abstract class AuditableBaseEntity<T> : BaseEntity<T>, IAuditable
    {
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
        public Guid ModifiedBy { get; set; }
        public DateTimeOffset ModifiedOn { get; set; } = DateTimeOffset.Now;
    }
}
