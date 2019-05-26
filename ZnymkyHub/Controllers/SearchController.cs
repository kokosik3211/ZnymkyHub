using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using ZnymkyHub.DAL.Core;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DTO.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZnymkyHub.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SearchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("{selectedCity}/{selectedPhType}")]
        public IActionResult PerformSearch(string selectedCity, string selectedPhType)
        {
            selectedPhType = selectedPhType.ToLower().Replace(' ', '_');
            var photographers = _unitOfWork.Context.Photographers.Where(p => p.HomeTown == selectedCity && p.UserPhotoshootTypes.Any(v => v.PhotoshootType.Name == selectedPhType)).ToList();

            List<SimpleSearchResultDTO> searchResult = new List<SimpleSearchResultDTO>();
            Image<Rgba32> photo;
            foreach (var elem in photographers)
            {
                var photos = elem.Photos.Where(p => (p.PhotoshootType?.Name ?? " ") == selectedPhType).Take(4).ToList();
                var pht = elem.UserPhotoshootTypes.FirstOrDefault(v => v.PhotoshootType.Name == selectedPhType);
                int pric = pht.Price;

                string base64 = null;
                if (elem.ProfilePhoto != null)
                {
                    photo = Image.Load(elem.ProfilePhoto);
                    base64 = photo.ToBase64String(PngFormat.Instance);
                }

                searchResult.Add(new SimpleSearchResultDTO
                {
                    id = elem.Id,
                    firstName = elem.FirstName,
                    lastName = elem.LastName,
                    location = elem.HomeTown,
                    price = pric,
                    fullName = $"{elem.FirstName} {elem.LastName}",
                    base64 = base64,
                    photos = MapToSimplePhoto(photos)
                });
            }
            return Ok(searchResult);
        }

        private List<SimplePhotoDTO> MapToSimplePhoto(List<Photo> photos)
        {
            List<SimplePhotoDTO> simples = new List<SimplePhotoDTO>();
            Image<Rgba32> photo;
            foreach (var elem in photos)
            {
                photo = Image.Load(elem.PhotoResolution.Small);
                simples.Add(new SimplePhotoDTO
                {
                    id = elem.Id,
                    name = elem.Name,
                    numlikes = elem.NumberOfLikes,
                    numsaves = elem.NumberOfSaving,
                    base64 = photo.ToBase64String(PngFormat.Instance),
                    phtype = elem.PhotoshootType.Name
                });
            }

            return simples;
        }
    }
}
