using Microsoft.EntityFrameworkCore;
using OffersCRUD.Domain;
using OffersCRUD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OffersCRUD.Infrastructure.Repositiories
{
    public class OfferRepository : BaseRepository<Offer>, IOfferRepository
    {
        private readonly IBaseRepository<Offer> _offerRepository;
        private readonly IBaseRepository<ProductOffer> _productOfferRepository;
        public OfferRepository(IBaseRepository<Offer> offerRepository, IBaseRepository<ProductOffer> productOfferRepository, OfferDbContext context) : base(context)
        {
            _offerRepository = offerRepository;
            _productOfferRepository = productOfferRepository;
        }
        public async Task<CreateUpdateOfferDto> Add(CreateUpdateOfferDto entity)
        {
            try
            {
                ValidateDate(entity);
               //await ValidateOfferOnProduct(entity);
                //Photo photo = PreparePhoto(entity);
                //maaping product offers from Product IDs 
                IList<ProductOffer> productOffers = entity.ProductIds.Select(x => new ProductOffer() 
                { ProductId = x.ProductId, StoreId = x.StoreId, StartDate = DateTime.Parse( entity.StartDate), ExpireDate = DateTime.Parse(entity.ExpireDate )}).ToList();
                Offer offer = new(entity.Name, entity.Description, DateTime.Parse( entity.ExpireDate), entity.Status, entity.Image, productOffers);

                _offerRepository.Add(offer);
                _offerRepository.Save();
                entity.Id = offer.Id;
                return entity;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        

        public List<OfferDto> Get()
        {
            var offers = table.Include(x => x.ProductOffers).ThenInclude(x => x.Store).ToList();
            List<OfferDto> offerDtos = offers.Select(x => new OfferDto()
            {
                Id = x.Id,
                StoreName = x.ProductOffers!=null? x.ProductOffers.Count(y => y.StoreId != null) == 1 ? x.ProductOffers.FirstOrDefault(y => y.StoreId != null).Store.Name : null: null,
                Name = x.Name,
                Description = x.Description,

                StartDate = x.ProductOffers != null ? x.ProductOffers.FirstOrDefault().StartDate:null,
                ExpireDate = x.ProductOffers != null ? x.ProductOffers.FirstOrDefault().ExpireDate:null,
                Status = Enum.GetName(typeof(OfferStatus), x.Status)
            }).ToList();

            return offerDtos;
        }

        public OfferDto GetById(int id)
        {
            var offer= table.Include(x => x.ProductOffers).ThenInclude(x => x.Store).FirstOrDefault(x=>x.Id==id);
            OfferDto offerDto =  new OfferDto()
            {
                Id = offer.Id,
                StoreName = offer.ProductOffers != null ? offer.ProductOffers.Count(y => y.StoreId != null) == 1 ? offer.ProductOffers.FirstOrDefault(y => y.StoreId != null).Store.Name : null : null,
                Name = offer.Name,
                Description = offer.Description,
                StartDate = offer.ProductOffers != null ? offer.ProductOffers.FirstOrDefault().StartDate : null,
                ExpireDate = offer.ProductOffers != null ? offer.ProductOffers.FirstOrDefault().ExpireDate : null,
                Status = Enum.GetName(typeof(OfferStatus), offer.Status)
            };

            return offerDto;
        }
        public void Update(CreateUpdateOfferDto entity)
        {
            try
            {
                ValidateDate(entity);
                //ValidateOfferOnProduct(entity);
                //Photo photo = PreparePhoto(entity);
                //maaping product offers from Product IDs 
                IList<ProductOffer> productOffers = entity.ProductIds.Select(x => new ProductOffer()
                { ProductId = x.ProductId, StoreId = x.StoreId, StartDate = DateTime.Parse(entity.StartDate), OfferId = entity.Id.Value, ExpireDate = DateTime.Parse(entity.ExpireDate) }).ToList();
                Offer offer = _offerRepository.GetById(entity.Id.Value);
                offer.UpdateOffer(entity.Name, entity.Description, DateTime.Parse(entity.ExpireDate), entity.Status, entity.Image, productOffers);
                _offerRepository.Update(offer);
                _offerRepository.Save();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private async Task ValidateOfferOnProduct(CreateUpdateOfferDto entity)
        {
            var start = DateTime.Parse(entity.StartDate);
            var end = DateTime.Parse(entity.ExpireDate);
            var isExistOffer = false;
           
            //check overlap when update
            if (entity.Id.HasValue)
                isExistOffer = _productOfferRepository.Any(x => x.OfferId != entity.Id.Value && entity.ProductIds.Any(y => x.ProductId == y.ProductId || x.StoreId == y.StoreId)
               && ((x.StartDate <= start && x.StartDate <= end) || (x.ExpireDate <= start && x.ExpireDate <= end)));
            //check overlap when add
            else
                isExistOffer = _productOfferRepository.Any(x => entity.ProductIds.Any(y => x.ProductId == y.ProductId || x.StoreId == y.StoreId)
               && ((x.StartDate <= start && x.StartDate <= end) || (x.ExpireDate <= start && x.ExpireDate <= end)));

            if (isExistOffer)
                throw new Exception("There are offer on this period");
        }

        //private Photo PreparePhoto(CreateUpdateOfferDto entity)
        //{

        //    //define photo from dto
        //    Photo photo = new()
        //    {
        //        Name = entity.Image.Name,
        //        PhotoExtension = entity.Image.PhotoExtension,
        //        Bytes = entity.Image.Bytes

        //    };
        //    return photo;
        //}
        private static void ValidateDate(CreateUpdateOfferDto entity)
        {
            var start = DateTime.Parse(entity.StartDate);
            var end = DateTime.Parse(entity.ExpireDate);
            if (start.Date < DateTime.Now.Date)
                throw new Exception("start Date must be greater than or equal Today ");

            if (start.Date > end.Date)
                throw new Exception("start Date must be befor Expiry Date");
        }
    }
}
