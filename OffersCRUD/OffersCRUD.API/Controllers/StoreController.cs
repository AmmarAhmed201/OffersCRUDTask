using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OffersCRUD.Domain;
using OffersCRUD.Services.Dtos;
using OffersCRUD.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffersCRUD.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowOrigin")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        [HttpGet]
        [Route("GetAllStores")]
        public List<ProductStoreDto> GetAllStores()
        {
            var storeDtos = _storeRepository.Get().Select(s => new ProductStoreDto { Id = s.Id, name = s.Name }).ToList();

            return storeDtos;
        }
    }

}
