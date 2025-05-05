
using Dunnhumby.RestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Dunnhumby.RestApi.Context
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }



        // If we need to create refrences or relations with other tables or constraints etc use following as refrence
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Product>()
        //        .ToTable("Products");

        //}

    }
}

