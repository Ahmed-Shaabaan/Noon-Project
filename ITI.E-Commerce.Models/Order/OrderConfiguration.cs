using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(x=>x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.OrderDate).IsRequired();
            builder.Property(x => x.ShipperID).IsRequired(false);
            builder.Property(x => x.CustomerID).IsRequired(false);
            builder.Property(x => x.DeliveryCost).IsRequired();
            builder.Property(x => x.ShippedDate).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.AddressID).IsRequired();    
            //builder.Property(x => x.OrderDetails).IsRequired(); 
        }
    }
}
