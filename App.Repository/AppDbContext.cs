using System;
using System.Reflection;
using App.Core.Entities;
using App.Repository.Configurations;
using Microsoft.EntityFrameworkCore;

namespace App.Repository
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{

		}

		public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.ApplyConfiguration(new ProductConfiguration()); 

            modelBuilder.Entity<ProductFeature>().HasData
            (
                new ProductFeature(){Id=1, Color="mavi", Height=100, Width=200, ProductId=1},
                new ProductFeature(){Id=2, Color="kirmizi", Height=300, Width=500, ProductId=2}
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}

