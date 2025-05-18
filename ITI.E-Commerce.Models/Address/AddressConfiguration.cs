using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.E_Commerce.Models
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>

    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.Street).IsRequired();
            builder.Property(x => x.OrderID).IsRequired();
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.Zipcode).IsRequired();


        }
    }
}
