using Application.Contexts;
using Application.DTOs.ProductDtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Product.CRUD
{
    public interface IEditProduct
    {
        Task EditProductAsync(int productId, ProductDto updatedProductDto);
    }

    public class EditProduct : IEditProduct
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public EditProduct(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task EditProductAsync(int productId, ProductDto updatedProductDto)
        {
          
            var existingProduct = await _context.Products.FindAsync(productId);

            if (existingProduct == null)
            {
                throw new Exception($"Product with ID {productId} not found.");
            }

         
            _mapper.Map(updatedProductDto, existingProduct);

         
            await _context.SaveChangesAsync();
        }
    }
}
