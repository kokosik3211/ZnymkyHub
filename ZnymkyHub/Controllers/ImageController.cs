using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;
using SixLabors.ImageSharp.Formats.Png;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ZnymkyHub.Domain.Models;
using ZnymkyHub.Infrastructure.EF.Entities;
using ZnymkyHub.Infrastructure.EF;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZnymkyHub.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Consumes("application/json", "multipart/form-data")]
    public class ImageController : Controller
    {
        private readonly ZnymkyHubContext _dbContext;
        private readonly ClaimsPrincipal _caller;


        public ImageController(ZnymkyHubContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPost]
        public IActionResult GetImage()
        {
            var kek = _dbContext.PhotoResolutions.Find(1).Original;
            var lol = _dbContext.PhotoResolutions.Where(p => p.Id == 1).Select(p => new { Id = p.Id, Small = p.Small }).FirstOrDefault();
            Image<Rgba32> image = Image.Load(lol.Small);
            string base64 = image.ToBase64String(PngFormat.Instance);
            return new OkObjectResult(base64);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> ChangeUserImage([FromRoute]int id, [FromForm]IFormFile newimage)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await newimage.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            Image<Rgba32> imageToSave = Image.Load(fileBytes);
            imageToSave = MakeImgSquare(imageToSave);
            byte[] newphoto = ImgToByteArray(imageToSave, PngFormat.Instance);

            var user = _dbContext.Users.Find(id);
            user.ProfilePhoto = newphoto;
            await _dbContext.SaveChangesAsync();

            var imageToSend = _dbContext.Users.Find(id).ProfilePhoto;

            Image<Rgba32> image = Image.Load(imageToSend);
            string base64 = image.ToBase64String(PngFormat.Instance);
            return new OkObjectResult(base64);
        }

        [Authorize]
        [HttpPost]
        [Route("{name}/{pType}")]
        public async Task<IActionResult> UploadPhoto(string name, string pType, [FromForm]IFormFile newimage)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await newimage.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            Image<Rgba32> imageToSave = Image.Load(fileBytes);
            var resizedImgs = ResizeImg(imageToSave);

            var userId = _caller.Claims.Single(c => c.Type == "id");
            pType = pType.ToLower().Replace(' ', '_');
            var pTypeId = _dbContext.PhotoshootTypes.FirstOrDefault(x => x.Name == pType).Id;

            var photo = new Photo
            {
                Name = name,
                PhotographerId = Convert.ToInt32(userId.Value),
                PhotoshootTypeId = pTypeId,
                DateTime = DateTime.Now,
                NumberOfLikes = 0,
                NumberOfSaving = 0
            };
            _dbContext.Photos.Add(photo);
            await _dbContext.SaveChangesAsync();

            _dbContext.PhotoResolutions.Add(new PhotoResolution
            {
                PhotoId = photo.Id,
                Original = ImgToByteArray(resizedImgs.Item1, PngFormat.Instance),
                Medium = ImgToByteArray(resizedImgs.Item2, PngFormat.Instance),
                Small = ImgToByteArray(resizedImgs.Item3, PngFormat.Instance)
            });
            await _dbContext.SaveChangesAsync();
            //byte[] newphoto = ImgToByteArray(imageToSave, PngFormat.Instance);

            return Ok("suka added");
        }

        [HttpPost]
        [Route("{id}/{current}")]
        public IActionResult GetPhotosForPhotoArea([FromRoute]int id, [FromRoute]int current)
        {
            var photographer = _dbContext.Photographers.Find(id);
            var photos = photographer.Photos.Reverse().Skip((current - 1) * 15).Take(15);

            var originals = new List<SimplePhoto>();
            Image<Rgba32> photo;
            foreach(var elem in photos)
            {
                photo = Image.Load(elem.PhotoResolution.Small);
                originals.Add(new SimplePhoto {
                    id = elem.Id,
                    name = elem.Name,
                    base64 = photo.ToBase64String(PngFormat.Instance),
                    numlikes = elem.NumberOfLikes,
                    numsaves = elem.NumberOfSaving,
                    phtype = elem.PhotoshootType?.Name
                });
            }

            return new OkObjectResult(originals);
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult GetInfoForPhotoPaginator([FromRoute]int id)
        {
            int count = _dbContext.Photos.Count(p => p.PhotographerId == id);
            return Ok(count);
        }

        private Image<Rgba32> MakeImgSquare(Image<Rgba32> img)
        {
            if(img.Height > img.Width)
            {
                img.Mutate(x => x.Crop(new Rectangle(0, (int)(img.Height - img.Width) / 2, img.Width, img.Width)));
            }
            else
            {
                img.Mutate(x => x.Crop(new Rectangle((int)(img.Width - img.Height) / 2, 0, img.Height, img.Height)));
            }

            img.Save($@"/Users/vova/Desktop/testCroppedPhoto.jpg");
            return img;
        }

        private byte[] ImgToByteArray(Image<Rgba32> source, IImageFormat format)
        {
            using (var stream = new MemoryStream())
            {
                source.Save(stream, format);
                stream.Flush();
                //return $"data:{format.DefaultMimeType};base64,{Convert.ToBase64String(stream.ToArray())}";
                return stream.ToArray();
            }
        }

        private (Image<Rgba32>, Image<Rgba32>, Image<Rgba32>) ResizeImg(Image<Rgba32> image)
        {
            Image<Rgba32> original = image.Clone();
            Image<Rgba32> medium = image.Clone();
            Image<Rgba32> small = image.Clone();

            original.Mutate(x => x.Resize(image.Width, image.Height));
            medium.Mutate(x => x.Resize((int)(image.Width / 2), (int)(image.Height / 2)));
            small.Mutate(x => x.Resize((int)(image.Width / 3), (int)(image.Height / 3)));

            return (original, medium, small);
        }
    }
}
