using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OffersCRUD.Domain;

namespace OffersCRUD.Infrastructure
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
          
            //// relation 1:many with images 
            //builder.HasMany<Photo>(p => p.Images)
            //    .WithOne(ph => ph.Product)
            //    .HasForeignKey(ph => ph.ProductId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //relation many:1 with store
            builder.HasOne<Store>(p => p.Store)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Cascade);


            //add product seed 
            builder.HasData(
                new Product {Id=1, Name = " Iphone 13 Mobile", StoreId = 1, Price = 10000.00 },
                new Product { Id = 2, Name = " Samsung s 10 Mobile", StoreId = 1, Price = 80000.00 },
                new Product { Id = 3, Name = "mi 11 Mobile", StoreId = 2, Price = 4000.00 },
                new Product { Id = 4, Name = " LG Smart", StoreId = 2, Price = 20000.00 },
                new Product { Id = 5, Name = " Maths Books", StoreId = 3, Price = 1000.00 },
                new Product { Id = 6, Name = " Coffe Machine", StoreId = 3, Price = 9000.00 },
                new Product { Id = 7, Name = " Ipad", StoreId = 4, Price = 2000.00 },
                new Product { Id = 8, Name = "Swimming tools", StoreId = 4, Price = 300.00 }
            );
        }
    }
}
