using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductInventory.Application.CQRS.Commands;
using ProductInventory.Application.CQRS.Queries;
using ProductInventory.Application.DTOs;

namespace ProductInventory.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _mediator.Send(new GetProductById.Query { Id = id });
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Command command)
        {
            var product = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(Guid id, [FromBody] UpdateProduct.Command command)
        {
            if (id != command.Id)
                return BadRequest();

            var product = await _mediator.Send(command);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _mediator.Send(new DeleteProduct.Command { Id = id});
            return NoContent();
        }
    }
}
