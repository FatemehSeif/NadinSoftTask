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
    public interface IProductFilter
    {
        Task<List<ProductDto>> GetProductsByUserAsync(string userId);
    }
    public class ProductFilter : IProductFilter
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public ProductFilter(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetProductsByUserAsync(string userId)
        {
            var products = await _context.Products
                .Include(p => p.CreatedBy) 
                .Where(p => p.CreatedById == userId)
                .ToListAsync();

            
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
