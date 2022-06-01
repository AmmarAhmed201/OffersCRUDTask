using Microsoft.AspNetCore.Http;
using OffersCRUD.Domain;
using System.Collections.Generic;

namespace OffersCRUD.Services
{
    public class CreateUpdateOfferDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string StartDate { get; set; }
        public string ExpireDate { get; set; }
        public OfferStatus Status { get; set; }
        public string Image { get; set; }
        public List<StoreProductDto> ProductIds { get; set; }
    } 

}
