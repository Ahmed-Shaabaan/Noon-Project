using ITI.E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ITI.E_Commerce
{
    public static class RelationMapping
    {
        public static void MappRelationships(this ModelBuilder builder)
        {
            builder.Entity<Product>().
                    HasOne(i => i.Category)
                    .WithMany(i => i.Products).HasForeignKey(i => i.CategoryID)
                    .IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>().
                    HasOne(i => i.Brand)
                    .WithMany(i => i.Products).HasForeignKey(i => i.BrandID)
                    .IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Rate>().
                   HasOne(i => i.Product)
                   .WithMany(i => i.Rates).HasForeignKey(i => i.ProductID)
                   .IsRequired().OnDelete(DeleteBehavior.Cascade);


            builder.Entity<ProductImages>().
             HasOne(i => i.Products)
             .WithMany(i => i.ProductImages).HasForeignKey(i => i.ProductID)
             .IsRequired().OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<Order>().HasOne(i => i.Customers).WithMany(i => i.Orders)
            //    .HasForeignKey(i => i.ShipperID)
            //    .IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>().HasOne(i => i.Customers).WithMany(i => i.Orders)
               .HasForeignKey(i => i.CustomerID)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Order>().HasOne(i => i.Shippers).
                WithMany(i => i.Orders_Shipper)
                .HasForeignKey(i => i.ShipperID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<OrderDetails>().HasOne(i => i.Products).WithMany(i => i.OrderDetails)
            .HasForeignKey(i => i.ProductID)
            .IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderDetails>().HasOne(i => i.Orders).WithMany(i => i.OrderDetails)
            .HasForeignKey(i => i.OrderID)
            .IsRequired().OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<Address>().HasOne(i=> i.Order).WithOne(i => i.ShippingAddress)
                .HasForeignKey<Address>(i => i.OrderID);

            builder.Entity<Order>().HasOne(i => i.ShippingAddress).WithOne(i => i.Order)
                .HasForeignKey<Order>(i => i.AddressID);




        }
    }
}
