namespace DDDTemplate.Domain.Events
{
    public class ProductPriceChangedEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public decimal OldPrice { get; }
        public decimal NewPrice { get; }
        public DateTime OccurredOn { get; }

        public ProductPriceChangedEvent(Guid productId, decimal oldPrice, decimal newPrice)
        {
            ProductId = productId;
            OldPrice = oldPrice;
            NewPrice = newPrice;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
