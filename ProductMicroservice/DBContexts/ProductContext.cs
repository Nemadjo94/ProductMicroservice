using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroservice.DBContexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        const string hiloName = "product_hilo";

        const string productTable = "Products";
        const string categoriesTable = "Categories";

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        //Configure each of our entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseSequenceHiLo(hiloName);
            var sequence = modelBuilder.HasSequence(hiloName);
            sequence.IncrementsBy(100);
            sequence.StartsAt(1000);

            modelBuilder.Entity<Product>(ConfigureProduct);
            modelBuilder.Entity<Category>(ConfigureCategory);

           
        }

        //Product Entity configuration
        void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(productTable);

            builder.HasKey(key => key.Id);

            builder.Property(prop => prop.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(prop => prop.Description)
                .IsRequired(true)
                .HasMaxLength(1000);

            builder.Property(prop => prop.Price)
                .IsRequired(true);

            builder.Property(prop => prop.CategoryId)
                .IsRequired(true);
        }

        //Category Entity configuration
        void ConfigureCategory(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(categoriesTable);

            builder.HasKey(key => key.Id);

            builder.Property(prop => prop.Name)
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(prop => prop.Description)
                .HasMaxLength(1000)
                .IsRequired(true);
        }

       


    }
}
