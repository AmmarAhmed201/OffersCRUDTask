using System;

namespace OffersCRUD.Domain
{
    public class ProductOffer : BaseEntity
    {

        public DateTime StartDate { get;  set; }
        public DateTime ExpireDate { get;  set; }
        public int? StoreId { get; set; }
        public Store Store { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
