using OffersCRUD.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OffersCRUD.Domain
{
    public interface IOfferRepository : IBaseRepository<Offer>
    {
        Task<CreateUpdateOfferDto> Add(CreateUpdateOfferDto entity);
        void Update(CreateUpdateOfferDto entity);
        List<OfferDto> Get();
        OfferDto GetById(int id);
    }
}
