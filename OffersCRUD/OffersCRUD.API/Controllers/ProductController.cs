using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OffersCRUD.Services.Dtos;
using OffersCRUD.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("[controller]")]
[EnableCors("AllowOrigin")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    [HttpGet]
    [Route("GetAllProducts")]
    public List<ProductStoreDto> GetAllProduct()
    {
        var storeDtos = _productRepository.Get().Select(s => new ProductStoreDto { Id = s.Id, name = s.Name }).ToList();

        return storeDtos;
    }
}
