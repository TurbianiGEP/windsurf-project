using DDDTemplate.Domain.Events;
using DDDTemplate.Domain.Exceptions;
using DDDTemplate.Domain.ValueObjects;

namespace DDDTemplate.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }

        private Product() { } // For EF Core

        public Product(string name, string description, Money price, int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ProductDomainException("Product name cannot be empty");
            
            if (price == null)
                throw new ProductDomainException("Product price cannot be null");

            if (stockQuantity < 0)
                throw new ProductDomainException("Stock quantity cannot be negative");

            Name = name;
            Description = description ?? string.Empty;
            Price = price;
            StockQuantity = stockQuantity;
            IsActive = true;

            AddDomainEvent(new ProductCreatedEvent(Id, Name, Price.Amount));
        }

        public void UpdatePrice(Money newPrice)
        {
            if (newPrice == null)
                throw new ProductDomainException("Product price cannot be null");

            var oldPrice = Price.Amount;
            Price = newPrice;
            MarkAsUpdated();

            AddDomainEvent(new ProductPriceChangedEvent(Id, oldPrice, newPrice.Amount));
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0)
                throw new ProductDomainException("Stock quantity cannot be negative");

            StockQuantity = quantity;
            MarkAsUpdated();
        }

        public void Deactivate()
        {
            IsActive = false;
            MarkAsUpdated();
        }

        public void Activate()
        {
            IsActive = true;
            MarkAsUpdated();
        }
    }
}
