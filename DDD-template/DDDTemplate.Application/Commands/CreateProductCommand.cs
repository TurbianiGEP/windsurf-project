using DDDTemplate.Application.DTOs;

namespace DDDTemplate.Application.Commands
{
    public class CreateProductCommand : ICommand<ProductDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD";
        public int StockQuantity { get; set; }
    }
}
