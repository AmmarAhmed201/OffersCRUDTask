using System.Collections.Generic;

namespace OffersCRUD.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public virtual string ImageUrl { get; set; }
    }
}