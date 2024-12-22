using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain.Models;
using Application.Contexts;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Presistance.Context
{
    public class DataBaseContext : DbContext, Application.Contexts.IDataBaseContext 
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

     
        public EntityEntry Entry([NotNull] object entity)
        {
            return base.Entry(entity);
        }

       
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<Product>()
                        .HasIndex(p => new { p.ProduceDate, p.ManufactureEmail })
                        .IsUnique();
            modelBuilder.Entity<Product>()
                              .HasOne(p => p.CreatedBy)
                              .WithMany(u => u.Products)
                              .HasForeignKey(p => p.CreatedById)
                              .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
