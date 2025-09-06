using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.DTOs;
using Product.Application.Commands.CreateProduct;
using Product.Application.Commands.UpdateProduct;
using Product.Application.Commands.DeleteProduct;
using Product.Application.Queries.GetProductById;
using Product.Application.Queries.GetAllProducts;
using Product.Application.Queries.GetProductByCode;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }

        // GET: api/product/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET: api/product/code/{productCode}
        [HttpGet("code/{productCode}")]
        public async Task<ActionResult<ProductDto>> GetByCode(string productCode)
        {
            var result = await _mediator.Send(new GetProductByCodeQuery(productCode));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductCommand command)
        {
            var createdId = await _mediator.Send(command);
            return Ok(createdId);
        }

        // PUT: api/product/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Product.Id) return BadRequest("Product ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand{ Id = id });
            return NoContent();
        }
    }
}