using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate> 
    {
       

        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.ToTable("Rate");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.ProductID).IsRequired();
            builder.Property(i => i.NumberOfStart).IsRequired();
            builder.Property(i => i.CustomerID).IsRequired();
            builder.Property(i => i.Date).IsRequired().ValueGeneratedOnAdd();

        }
    }
}
