using Microsoft.EntityFrameworkCore;
using OffersCRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffersCRUD.Infrastructure
{
    public class OfferDbContext : DbContext
    {
        public OfferDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfferDbContext).Assembly);
        }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products {  get; set; }
        //public DbSet<Photo> Images {  get; set; }
        public DbSet<ProductOffer> ProductOffers {  get; set; }
    }
}
