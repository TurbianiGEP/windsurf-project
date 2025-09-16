using Microsoft.AspNetCore.Mvc;
using DDDTemplate.Application.Commands;
using DDDTemplate.Application.Queries;
using DDDTemplate.Application.DTOs;

namespace DDDTemplate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ICommandHandler<CreateProductCommand, ProductDto> _createProductHandler;
        private readonly IQueryHandler<GetProductByIdQuery, ProductDto> _getProductByIdHandler;

        public ProductsController(
            ICommandHandler<CreateProductCommand, ProductDto> createProductHandler,
            IQueryHandler<GetProductByIdQuery, ProductDto> getProductByIdHandler)
        {
            _createProductHandler = createProductHandler;
            _getProductByIdHandler = getProductByIdHandler;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand command)
        {
            try
            {
                var result = await _createProductHandler.HandleAsync(command);
                return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            try
            {
                var query = new GetProductByIdQuery(id);
                var result = await _getProductByIdHandler.HandleAsync(query);
                
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
