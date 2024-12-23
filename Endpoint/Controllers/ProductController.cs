using Application.DTOs.ProductDtos;
using Application.Services.Account;
using Application.Services.Product.CRUD.Commands;
using Application.Services.Product.CRUD.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IAddProductCommand _addProductHandler;
        private readonly IEditProductCommandHandler _editProductHandler;
        private readonly IDeleteProductCommandHandler _deleteProductHandler;
        private readonly IGetProductsByUserQueryHandler _getProductsByUserHandler;
        private readonly IMediator _mediator;
        private readonly GetProductByIdQuery getProductByIdQuery;

        public ProductController(
            IMediator mediator,
            IAddProductCommand addProductHandler,
            IEditProductCommandHandler editProductHandler,
            IDeleteProductCommandHandler deleteProductHandler,
            IGetProductsByUserQueryHandler getProductsByUserHandler
            )
        {
            _addProductHandler = addProductHandler;
            _editProductHandler = editProductHandler;
            _deleteProductHandler = deleteProductHandler;
            _getProductsByUserHandler = getProductsByUserHandler;
            _mediator = mediator;
            
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data cannot be null.");
            }

            await _addProductHandler.ExecuteAsync(productDto);
            return Ok("Product added successfully.");
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> EditProduct(int productId, [FromBody] ProductDto updatedProductDto)
        {
            var command = new EditProductCommand(productId, updatedProductDto);
            await _mediator.Send(command);
            return Ok("Product updated successfully.");
        }


        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var command = new DeleteProductCommand(productId);
            await _mediator.Send(command);
            return Ok("Product deleted successfully.");
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProductsByUser(string userId)
        {
            var query = new GetProductsByUserQuery(userId);
            var products = await _getProductsByUserHandler.HandleAsync(query);
            return Ok(products);
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var query = new GetProductByIdQuery(productId);
            var product = await _mediator.Send(query);
            return Ok(product);
        }
    }

}

