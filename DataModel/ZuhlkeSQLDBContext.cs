using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class ZuhlkeSQLDBContext : DbContext, IDatabaseContext
    {
        private readonly ZuhlkeDBContextOptions _options;

        public ZuhlkeSQLDBContext(ZuhlkeDBContextOptions options) 
            : base(options.Options)
        {
            _options = options;
        }
        
        public DbSet<Sales> Sales { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales>().Ignore(x => x.IsValid);
            modelBuilder.Entity<Sales>().Ignore(x => x.RowID);
            modelBuilder.Entity<Sales>().Ignore(x => x.LineNumber);

            modelBuilder.Entity<Sales>()
                .Property(x => x.Id).HasColumnName("ID");

            modelBuilder.Entity<Sales>()
                .Property(x => x.OrderId).HasColumnName("ORDER_ID").IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(x => x.OrderDate).HasColumnName("ORDER_DATE").IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(x => x.ShipDate).HasColumnName("SHIP_DATE").IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(x => x.ShipMode).HasColumnName("SHIP_MODE");

            modelBuilder.Entity<Sales>()
                .Property(x => x.Quantity).HasColumnName("QUANTITY").IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(x => x.Discount).HasColumnName("DISCOUNT");

            modelBuilder.Entity<Sales>()
                .Property(x => x.Profit).HasColumnName("PROFIT").IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(x => x.ProductId).HasColumnName("PRODUCT_ID").IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(x => x.CustomerName).HasColumnName("CUSTOMER_NAME").IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(x => x.Category).HasColumnName("CATEGORY").IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(x => x.CustomerID).HasColumnName("CUSTOMER_ID").IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(x => x.ProductName).HasColumnName("PRODUCT_NAME");
            
        }
    }
}
