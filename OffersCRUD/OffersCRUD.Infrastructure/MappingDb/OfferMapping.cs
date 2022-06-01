using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OffersCRUD.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffersCRUD.Infrastructure
{
    public class OfferMapping : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();
            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.Description)
              .IsRequired()
              .HasMaxLength(500);

            ////relation 1:1 with image 
            //builder.HasOne(o => o.Image)
            //    .WithOne(i => i.Offer)
            //    .HasForeignKey<Offer>(o=>o.ImageId);

        }
    }
}
