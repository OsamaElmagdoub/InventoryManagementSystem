using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InventoryManagementSystem.Data
{
    public class Context  : DbContext
    {
       public DbSet<Product> Products { get; set; }
       public DbSet<User> Users { get; set; }
       public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
       public DbSet<Stock> Stocks { get; set; }
       public DbSet<StockTransfer> StockTransfers { get; set; }
       public DbSet<Warehouse> Warehouses { get; set; }
       public DbSet<WarehouseProduct> WarehouseProducts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=InventoryManagementSystem;Trusted_Connection=true;TrustServerCertificate=true")
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<WarehouseProduct>().HasNoKey();
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WarehouseProduct>()
         .HasKey(wp => new { wp.ProductId, wp.WarehouseId }); 

            modelBuilder.Entity<WarehouseProduct>()
                .HasOne(wp => wp.Product)
                .WithMany(p => p.WarehouseProducts)
                .HasForeignKey(wp => wp.ProductId);

            modelBuilder.Entity<WarehouseProduct>()
                .HasOne(wp => wp.Warehouse)
                .WithMany(w => w.WarehouseProducts)
                .HasForeignKey(wp => wp.WarehouseId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
