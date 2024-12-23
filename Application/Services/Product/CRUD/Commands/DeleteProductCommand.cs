using Application.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Product.CRUD.Commands
{
    public class DeleteProductCommand
    {
        public int ProductId { get; set; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }
    }
    public interface IDeleteProductCommandHandler
    {
        Task ExecuteAsync(DeleteProductCommand command);
    }

    public class DeleteProductCommandHandler : IDeleteProductCommandHandler
    {
        private readonly IDataBaseContext _context;

        public DeleteProductCommandHandler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(DeleteProductCommand command)
        {
         
            var product = await _context.Products.FindAsync(command.ProductId);

            if (product == null)
            {
                throw new Exception($"Product with ID {command.ProductId} not found.");
            }

    
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
    }
}
