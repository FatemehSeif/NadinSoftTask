using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Domain.Models;


namespace Presistance.Context
{
    public class DataBaseContext : DbContext , IDataBaseContext 
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }   
        DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
             .HasIndex(p => new { p.ProduceDate, p.ManufactureEmail })
             .IsUnique();
        }

    }
}
