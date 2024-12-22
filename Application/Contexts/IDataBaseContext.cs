using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
namespace Application.Contexts
{
    public interface IDataBaseContext
    {
         public DbSet<Product> Products { get; set; }

        EntityEntry Entry([NotNull] object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}