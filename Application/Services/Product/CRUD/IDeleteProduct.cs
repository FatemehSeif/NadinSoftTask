using Application.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Product.CRUD
{
    public interface IDeleteProduct
    {
        Task DeleteProductAsync(int productId);
    }

    public class DeleteProduct : IDeleteProduct
    {
        private readonly IDataBaseContext _context;

        public DeleteProduct(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task DeleteProductAsync(int productId)
        {
            // پیدا کردن محصول موجود
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                throw new Exception($"Product with ID {productId} not found.");
            }

      
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
    }
}
