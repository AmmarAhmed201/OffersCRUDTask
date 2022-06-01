using Microsoft.AspNetCore.Http;
using System;

namespace OffersCRUD.Services
{
    public class OfferDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Status { get; set; }
        public IFormFile Image { get; set; }
        public string StoreName { get; set; }
    }

}
