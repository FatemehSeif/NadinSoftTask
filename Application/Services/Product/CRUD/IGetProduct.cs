using Application.Contexts;
using Application.DTOs.ProductDtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Product.CRUD
{
    public interface IGetProduct
    {
        Task<List<ProductDto>> GetAllProductsAsync();
    }

    public class GetProduct : IGetProduct
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public GetProduct(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }

}
