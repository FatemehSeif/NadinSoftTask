using Application.Contexts;
using Application.DTOs.ProductDtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Product.CRUD.Commands
{
    public class EditProductCommand
    {
        public int ProductId { get; set; }
        public ProductDto UpdatedProductDto { get; set; }

        public EditProductCommand(int productId, ProductDto updatedProductDto)
        {
            ProductId = productId;
            UpdatedProductDto = updatedProductDto;
        }
    }
    public interface IEditProductCommandHandler
    {
        Task ExecuteAsync(EditProductCommand command);
    }


    public class EditProductCommandHandler : IEditProductCommandHandler
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public EditProductCommandHandler(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(EditProductCommand command)
        {
            var existingProduct = await _context.Products.FindAsync(command.ProductId);
            if (existingProduct == null)
            {
                throw new Exception($"Product with ID {command.ProductId} not found.");
            }
            _mapper.Map(command.UpdatedProductDto, existingProduct);
            await _context.SaveChangesAsync();
        }
    }
}
