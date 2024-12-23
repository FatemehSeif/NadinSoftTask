using Application.Contexts;
using Application.DTOs.ProductDtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Product.CRUD.Queries
{
    public class GetProductsByUserQuery
    {
        public string UserId { get; set; }

        public GetProductsByUserQuery(string userId)
        {
            UserId = userId;
        }
    }
    public interface IGetProductsByUserQueryHandler
    {
        Task<List<ProductDto>> HandleAsync(GetProductsByUserQuery query);
    }

    public class GetProductsByUserQueryHandler : IGetProductsByUserQueryHandler
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public GetProductsByUserQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> HandleAsync(GetProductsByUserQuery query)
        {
           
            var products = await _context.Products
                .Include(p => p.CreatedBy) 
                .Where(p => p.CreatedById == query.UserId)
                .ToListAsync();

          
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
