using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    
    
        public class BrandConfigration : IEntityTypeConfiguration<Brand>
        {
            public void Configure(EntityTypeBuilder<Brand> builder)
            {

                builder.ToTable("Brand");
                builder.HasKey(i => i.Id);
                builder.Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
                builder.Property(i => i.Name).IsRequired().HasMaxLength(100);
                builder.Property(i => i.Image).IsRequired();
            }
        }
    
}
