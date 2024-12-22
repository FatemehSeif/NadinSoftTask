using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Application.Contexts;

namespace Presistance.Context
{
    public class DataBaseContext : DbContext , IDataBaseContext 
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }   
       public  DbSet<Product> Products { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
             .HasIndex(p => new { p.ProduceDate, p.ManufactureEmail })
             .IsUnique();
        }

        EntityEntry IDataBaseContext.Entry(object entity)
        {
            throw new NotImplementedException();
        }

        int IDataBaseContext.SaveChanges()
        {
            throw new NotImplementedException();
        }

        int IDataBaseContext.SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new NotImplementedException();
        }

        Task<int> IDataBaseContext.SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<int> IDataBaseContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
