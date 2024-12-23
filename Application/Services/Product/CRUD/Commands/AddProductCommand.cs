using Application.Contexts;
using Application.DTOs.ProductDtos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Product.CRUD.Commands
{

    public interface IAddProductCommand
    {
        Task ExecuteAsync(ProductDto productDto);
    }

    public class AddProductCommand : IAddProductCommand
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public AddProductCommand(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Domain.Models.Product>(productDto);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
    }
}
