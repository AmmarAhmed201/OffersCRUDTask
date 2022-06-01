using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OffersCRUD.Domain;
using OffersCRUD.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;

using System.Threading.Tasks;


namespace OffersCRUD.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowOrigin")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferRepository _offerRepository;
        private IHostEnvironment _environment;

        public OfferController(IOfferRepository offerRepository, IHostEnvironment environment)
        {
           _environment = environment;
            _offerRepository = offerRepository;
        }
        [HttpGet]
        [Route("GetAll")]
        public List<OfferDto> GetList()
        {

            return _offerRepository.Get(); ;
        }
        [HttpGet]
        [Route("GetById")]
        public OfferDto GetById(int Id)
        {

            return _offerRepository.GetById(Id);
        }
      
        [HttpPost]
        [Route("AddOffer")]
        public CreateUpdateOfferDto Add([FromBody] CreateUpdateOfferDto OfferDto)
        {
            //call handling image
            OfferDto.Image = HandleCreateUpdateImage(OfferDto.Image, OfferDto.Name);
            _offerRepository.Add(OfferDto);
            return OfferDto;
        }

        [HttpPost]
        [Route("UpdateOffer")]
        public CreateUpdateOfferDto Update([FromBody] CreateUpdateOfferDto updateOffer)
        {
            updateOffer.Image = HandleCreateUpdateImage(updateOffer.Image, updateOffer.Name);
            _offerRepository.Update(updateOffer) ;
            return updateOffer;
        }
        [HttpDelete]
        [Route("DeleteOffer")]
        public void Delete(int offerId)
        {

            _offerRepository.Delete(offerId);
      
        }


        //handle upload image and return image url which save it in database 
        private string HandleCreateUpdateImage(string imageBase,string imageName)
        {
            //get image 
            string path = _environment.ContentRootPath; //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            imageName = imageName + ".jpg";

            //set the image path
            string imgPath = Path.Combine(path, imageName);
            //save image 
            var myString = imageBase.Split(new char[] { ',' });
            byte[] imageBytes = Convert.FromBase64String(myString[1]);

            System.IO.File.WriteAllBytes(imgPath, imageBytes);

            string onlinePath = $"https://localhost:44372/{imgPath}";
            return onlinePath;

        }
    }
}

