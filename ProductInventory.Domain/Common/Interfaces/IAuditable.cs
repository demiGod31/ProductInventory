namespace ProductInventory.Domain.Common.Interfaces
{
    internal interface IAuditable
    {
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
    }
}
