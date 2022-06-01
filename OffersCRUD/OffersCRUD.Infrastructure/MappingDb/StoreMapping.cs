using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OffersCRUD.Domain;

namespace OffersCRUD.Infrastructure
{
    public class StoreMapping : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Address)
                .IsRequired()
                .HasMaxLength(250);
            //seed data 
            builder.HasData(
                new Store { Id = 1, Name ="Cairo store",Address="Cairo"},
                new Store { Id = 2, Name ="Alex store",Address="Alex"},
                new Store { Id = 3, Name ="Tanta store",Address="Tanta"},
                new Store { Id = 4, Name ="Giza store",Address= "Giza" }
                );
        }
    }
}
