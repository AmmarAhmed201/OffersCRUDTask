using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OffersCRUD.Domain;

namespace OffersCRUD.Infrastructure
{
    public class ProductOfferMapping : IEntityTypeConfiguration<ProductOffer>
    {
        public void Configure(EntityTypeBuilder<ProductOffer> builder)
        {
            builder.HasKey(po => po.Id);
            builder.Property(po => po.Id)
            .ValueGeneratedOnAdd();

            //relation many:1 with offer

            builder.HasOne(pf => pf.Offer)
                .WithMany(f => f.ProductOffers)
                .HasForeignKey(pf => pf.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

            //relation many:1 with Product
            builder.HasOne(pf => pf.Product)
                    .WithMany()
                    .HasForeignKey(pf => pf.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);

            //relation many:1 with store 
            builder.HasOne(pf => pf.Store)
                    .WithMany()
                    .HasForeignKey(pf => pf.StoreId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
