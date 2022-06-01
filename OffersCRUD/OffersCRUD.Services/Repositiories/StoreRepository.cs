using OffersCRUD.Domain;
using OffersCRUD.Services.Interfaces;

namespace OffersCRUD.Infrastructure.Repositiories
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(OfferDbContext context) : base(context)
        {

        }
    }
}
