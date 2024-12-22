using Application.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Application.DTOs.ProductDtos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Application.Services.Product.CRUD
{
    public interface IAddProduct
    {
        Task AddProductAsync(ProductDto product);
    }
    public class AddProduct : IAddProduct
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public AddProduct(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Domain.Models.Product>(productDto);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
    }


}
