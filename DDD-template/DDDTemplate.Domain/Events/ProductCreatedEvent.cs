namespace DDDTemplate.Domain.Events
{
    public class ProductCreatedEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public string ProductName { get; }
        public decimal Price { get; }
        public DateTime OccurredOn { get; }

        public ProductCreatedEvent(Guid productId, string productName, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
