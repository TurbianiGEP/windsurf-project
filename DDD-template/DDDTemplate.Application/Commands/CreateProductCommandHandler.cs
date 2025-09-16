using DDDTemplate.Application.DTOs;
using DDDTemplate.Domain.Entities;
using DDDTemplate.Domain.Interfaces;
using DDDTemplate.Domain.ValueObjects;

namespace DDDTemplate.Application.Commands
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductDto>
    {
        private readonly IRepository<Product> _productRepository;

        public CreateProductCommandHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = default)
        {
            var money = new Money(command.Price, command.Currency);
            var product = new Product(command.Name, command.Description, money, command.StockQuantity);

            await _productRepository.AddAsync(product, cancellationToken);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.Amount,
                Currency = product.Price.Currency,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}
