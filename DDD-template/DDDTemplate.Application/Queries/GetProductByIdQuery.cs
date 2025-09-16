using DDDTemplate.Application.DTOs;

namespace DDDTemplate.Application.Queries
{
    public class GetProductByIdQuery : IQuery<ProductDto>
    {
        public Guid ProductId { get; set; }

        public GetProductByIdQuery(Guid productId)
        {
            ProductId = productId;
        }
    }
}
