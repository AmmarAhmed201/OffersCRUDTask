using OffersCRUD.Domain;
using OffersCRUD.Services.Interfaces;

namespace OffersCRUD.Infrastructure.Repositiories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(OfferDbContext context) : base(context)
        {
            
        }
    }
}
