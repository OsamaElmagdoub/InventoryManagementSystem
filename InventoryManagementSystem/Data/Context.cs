using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Mail;

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














//# Database configuration
//DATABASE_URL = Data Source = localhost; Database = Inventory_App; Trusted_Connection = True; Encrypt = false; TrustServerCertificate = true;


//# JWT Settings
//SECRET_KEY = xEx1Pnmr2KspDB6EWjkpxMn_QRD7DgWGnLuHuecz3zQ
//DURATION_IN_MINUTES = 30
//ISSUER = FoodApplication
//AUDIENCE = FoodApplicationUsers


//#Email Setting
//EMAIL_ADDRESS = marwa.ashm@yahoo.com
//EMAIL_PASSWORD = sfpn uibe poyr ixrc
//SENDER_NAME=Food Application
//EMAIL_HOST=smtp.mail.yahoo.com
//EMAIL_PORT = 587

