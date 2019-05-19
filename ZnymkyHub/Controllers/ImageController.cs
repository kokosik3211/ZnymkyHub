using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZnymkyHub.DAL.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;
using SixLabors.ImageSharp.Formats.Png;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZnymkyHub.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ImageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult GetImage()
        {
            var kek = _unitOfWork.PhotoResolutions.Get(1).Original;
            var lol = _unitOfWork.Context.PhotoResolutions.Where(p => p.Id == 1).Select(p => new { Id = p.Id, Small = p.Small }).FirstOrDefault();
            Image<Rgba32> image = Image.Load(lol.Small);
            string base64 = image.ToBase64String(PngFormat.Instance);
            return new OkObjectResult(base64);
        }
    }
}
