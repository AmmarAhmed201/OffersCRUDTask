using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffersCRUD.Domain
{
    public class Offer: BaseEntity
    {
        private Offer()
        {

        }

        public Offer(string name,string description, DateTime expireDate, OfferStatus status, string imageUrl,  IList<ProductOffer> productOffers)
        { 

            Name = name;
            Description = description;
            ExpireDate = expireDate;
            Status = status;
            ImageUrl = imageUrl;
            ProductOffers = productOffers;
        } 
        public void  UpdateOffer(string name,string description, DateTime expireDate, OfferStatus status, string imageUrl,  IList<ProductOffer> productOffers)
        { 
            Name = name;
            Description = description;
            ExpireDate = expireDate;
            Status = status;
            ImageUrl = imageUrl;
            ProductOffers = productOffers;
        }
        public string Name { get; private set; }

        public string Description { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public OfferStatus Status { get; private set; }
        public string ImageUrl { get; private set; }
        public ICollection<ProductOffer> ProductOffers { get; private set; }

    }
}
